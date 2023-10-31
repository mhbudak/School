using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace INFOIBV
{
    public partial class INFOIBV : Form
    {

        private Bitmap InputImage1;
        private Bitmap InputImage2;
        private Bitmap OutputImage;
        private Bitmap edgeImage;
        private int k_size;
        private double Sigma;
        private Bitmap equalizedImage;
        private int[] equalizedHistogram;
        private int[] equalizedCDF;
        private PictureBox selectedPictureBox; // Track the selected PictureBox
        List<(int r, int theta)> PeakList = new List<(int r, int theta)>();
        List<(Point start, Point end)> filteredSegments = new List<(Point start, Point end)>();



        public INFOIBV()
        {
            InitializeComponent();

            // Add Click event handlers for each PictureBox
            pictureBox1.Click += PictureBox_Click;
            pictureBox2.Click += PictureBox_Click;
            pictureBox3.Click += PictureBox_Click;
            pictureBox4.Click += PictureBox_Click;

        }

        private void LoadImageButton1_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openImageDialog.FileName;
                ImageFileName1.Text = file;
                InputImage1?.Dispose();

                InputImage1 = new Bitmap(file);
                if (InputImage1.Size.Height <= 0 || InputImage1.Size.Width <= 0 ||
                    InputImage1.Size.Height > 512 || InputImage1.Size.Width > 512)
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                {
                    pictureBox1.Image = (Image)InputImage1;
//                    Image1.Visible = false;
                }
            }
        }

        private void LoadImage2_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openImageDialog.FileName;
                ImageFileName2.Text = file;
                InputImage2?.Dispose();

                InputImage2 = new Bitmap(file);
                if (InputImage2.Size.Height <= 0 || InputImage2.Size.Width <= 0 ||
                    InputImage2.Size.Height > 512 || InputImage2.Size.Width > 512)
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                {
                    pictureBox2.Image = (Image)InputImage2;
//                    Image2.Visible = false;
                }
            }
        }
        /*
        * Conver_to_Gray_Click: process when user clicks "Convert to Gray" button
        */
        private void Conver_to_Gray_Click(object sender, EventArgs e)
        {
            
            if (InputImage1 == null) return;                                 // get out if no input image
            OutputImage?.Dispose();                                         // reset output image
            OutputImage = new Bitmap(InputImage1.Size.Width, InputImage1.Size.Height); // create new output image
            Color[,] ourImage = new Color[InputImage1.Size.Width, InputImage1.Size.Height]; // create array to speed-up operations (Bitmap functions are very slow)

            // copy input Bitmap to array            
            for (int x = 0; x < InputImage1.Size.Width; x++)                 // loop over columns
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)            // loop over rows
                {
                    ourImage[x, y] = InputImage1.GetPixel(x, y);              // set pixel color in array at (x,y)
                }
            }

            
            // ====================================================================
            // =================== YOUR FUNCTION CALLS GO HERE ====================
            // Alternatively we use buttons to invoke certain functionality
            // ====================================================================

            byte[,] workingImage = ConvertToGrayscale(ourImage);          // convert image to grayscale

            // ==================== END OF YOUR FUNCTION CALLS ====================
            // ====================================================================

            // copy array to output Bitmap
            for (int x = 0; x < workingImage.GetLength(0); x++)             // loop over columns
            {
                for (int y = 0; y < workingImage.GetLength(1); y++)         // loop over rows
                {
                    Color newColor = Color.FromArgb(workingImage[x, y], workingImage[x, y], workingImage[x, y]);
                    OutputImage.SetPixel(x, y, newColor);                  // set the pixel color at coordinate (x,y)
                }
            }

            pictureBox2.Image = (Image)OutputImage;                         // display output image

        }

        private byte[,] ConvertToGrayscale(Color[,] inputImage)
        {
            // create temporary grayscale image of the same size as input, with a single channel
            byte[,] tempImage = new byte[inputImage.GetLength(0), inputImage.GetLength(1)];

            // setup progress bar
            progressBar.Visible = true;
            progressBar.Minimum = 1;
            progressBar.Maximum = InputImage1.Size.Width * InputImage1.Size.Height;
            progressBar.Value = 1;
            progressBar.Step = 1;


            // process all pixels in the image
            for (int x = 0; x < InputImage1.Size.Width; x++)                 // loop over columns
                for (int y = 0; y < InputImage1.Size.Height; y++)            // loop over rows
                {
                    Color pixelColor = inputImage[x, y];                    // get pixel color
                   
                    byte weigtedGray = (byte)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);
                    //                   byte weigtedGray = (byte)(( pixelColor.R +  pixelColor.G + pixelColor.B)/3);
                    /* The basis for the color encoding used in analog television 
                     * in both the North American NTSC and the European PAL systems is used in calculation. 
                     * The luminance component is computed from the RGB components as under the assumption 
                     * that the RGB values have already been gamma corrected according to the TV encoding standard 
                     * (γNTSC = 2.2 and γPAL = 2.8) for playback.
                    */
                    tempImage[x, y] = weigtedGray;                           // set the new pixel color at coordinate (x,y)

//                    byte average = (byte)((pixelColor.R + pixelColor.B + pixelColor.G) / 3); // calculate average over the three channels
//                    tempImage[x, y] = average;                              // set the new pixel color at coordinate (x,y)

                    progressBar.PerformStep();                              // increment progress bar
                }
            progressBar.Visible = false;                                    // hide progress bar
            return tempImage;
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            // Handle the click event for any PictureBox
            selectedPictureBox = (PictureBox)sender;

            // Optionally, We can visually indicate the selected PictureBox (e.g., change its border color)
            HighlightSelectedPictureBox();
        }

        private void HighlightSelectedPictureBox()
        {
            // Remove highlighting from all PictureBoxes
            pictureBox1.BorderStyle = BorderStyle.None;
            pictureBox2.BorderStyle = BorderStyle.None;
            pictureBox3.BorderStyle = BorderStyle.None;
            pictureBox4.BorderStyle = BorderStyle.None;

            // Highlight the selected PictureBox by changing its border color
            selectedPictureBox.BorderStyle = BorderStyle.FixedSingle;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (selectedPictureBox == null || selectedPictureBox.Image == null)
            {
                MessageBox.Show("Please select an image to save by clicking on a PictureBox.");
                return;
            }

            // Show a SaveFileDialog with custom filter and options
            SaveFileDialog saveImageDialog = new SaveFileDialog();
            saveImageDialog.Filter = "Image Files|*.png;*.jpg;*.bmp|All Files|*.*";
            saveImageDialog.Title = "Save Image As";
            saveImageDialog.FileName = $"Image{selectedPictureBox.Tag}"; // Set a default filename based on the selected PictureBox
            saveImageDialog.FilterIndex = 1;

            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                // Save the selected image to the specified location
                selectedPictureBox.Image.Save(saveImageDialog.FileName);

                MessageBox.Show($"Image{selectedPictureBox.Tag} saved successfully.");
            }
        }

        private void Invers_Click(object sender, EventArgs e)
        {
            // Ensure an image is loaded in the PictureBox
            if (pictureBox1.Image != null)
            {
                Bitmap bitmap = new Bitmap(pictureBox1.Image);
                int width = bitmap.Width;
                int height = bitmap.Height;
                byte[,] grayValues = new byte[width, height];

                // Convert the image to grayscale values
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixelColor = bitmap.GetPixel(x, y);
                        byte grayValue = (byte)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);
                        /* The basis for the color encoding used in analog television 
                         * in both the North American NTSC and the European PAL systems is used in calculation. 
                         * The luminance component is computed from the RGB components as under the assumption 
                         * that the RGB values have already been gamma corrected according to the TV encoding standard 
                         * (γNTSC = 2.2 and γPAL = 2.8) for playback.
                         */
                        grayValues[x, y] = grayValue;
                    }
                }

                // Use the invert function
                byte[,] invertedValues = InvertImage(grayValues);

                // Apply the inverted values back to the image
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        byte invertedValue = invertedValues[x, y];
                        bitmap.SetPixel(x, y, Color.FromArgb(invertedValue, invertedValue, invertedValue));
                    }
                }

                // Refresh the PictureBox to show the inverted image
                pictureBox2.Image = bitmap;
                pictureBox2.Refresh();
            }
        }

        /*
         * invertImage: invert a single channel (grayscale) image
         */
        private byte[,] InvertImage(byte[,] inputImage)
        {
            int width = inputImage.GetLength(0);
            int height = inputImage.GetLength(1);

            byte[,] tempImage = new byte[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    byte pixelValue = inputImage[x, y];
                    tempImage[x, y] = (byte)(255 - pixelValue);  // Invert the grayscale value
                }
            }

            return tempImage;
        }

        private void Contrast_Adjustment_Click(object sender, EventArgs e)
        {
        //ContrastAdjusmentStart:
        // Ensure there's an image loaded in the PictureBox.
            if (pictureBox2.Image != null)
            {
                // Convert the image in the PictureBox to a Bitmap so we can manipulate its pixels.
                Bitmap bitmap = new Bitmap(pictureBox2.Image);

                // Get the width and height of the image for the loops.
                int width = bitmap.Width;
                int height = bitmap.Height;

                // Create a new 2D array to store the grayscale values of the image.
                byte[,] grayValues = new byte[width, height];

                // Loop through every pixel in the bitmap to get its color and convert it to grayscale.
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // Get the color of the current pixel.
                        Color pixelColor = bitmap.GetPixel(x, y);

                        // Convert the RGB color to a single grayscale value using standard weights.
                        byte grayValue = (byte)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);
                        /* calculate the basis for the color encoding 
                         * used in analog television in both the North American NTSC and the European PAL systems. 
                         * The luminance component is computed from the RGB components as under the assumption 
                         * that the RGB values have already been gamma corrected according to the TV encoding standard 
                         * (γNTSC = 2.2 and γPAL = 2.8) for playback.
                        */
                        grayValues[x, y] = grayValue;
                    }
                }

                // Use the adjustContrast function to get the contrast-adjusted grayscale values.
                byte[,] adjustedValues = AdjustContrast(grayValues);

                // Loop through every pixel again to apply the adjusted grayscale values to the image.
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        byte adjustedValue = adjustedValues[x, y];
                        bitmap.SetPixel(x, y, Color.FromArgb(adjustedValue, adjustedValue, adjustedValue));
                    }
                }

                // Update the PictureBox with the contrast-adjusted image and refresh it to display the changes.
                pictureBox2.Image = bitmap;
                pictureBox2.Refresh();
            }
        }

        /*
         * adjustContrast: create an image with the full range of intensity values used
         */
        private byte[,] AdjustContrast(byte[,] inputImage)
        {
            // Get the width and height of the image from its dimensions.
            int width = inputImage.GetLength(0);
            int height = inputImage.GetLength(1);

            // Create a temporary grayscale image with the same width and height.
            byte[,] tempImage = new byte[width, height];

            // Initialize min and max intensity values with opposite extremes.
            byte minIntensity = 255;
            byte maxIntensity = 0;

            // Find the current minimum and maximum intensity values in the image.
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    byte pixelValue = inputImage[x, y];
                    if (pixelValue < minIntensity) minIntensity = pixelValue;
                    if (pixelValue > maxIntensity) maxIntensity = pixelValue;
                }
            }

            // Adjust the contrast of the image based on the found min and max intensity values.
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    byte pixelValue = inputImage[x, y];

                    // Use the contrast stretching formula to adjust the pixel's intensity to 0-255.
                    // newValue = (value - min) * 255 / (max - min)
                    byte adjustedValue = (byte)((pixelValue - minIntensity) * 255 / (maxIntensity - minIntensity));

                    // Store the adjusted intensity value in the temporary image.
                    tempImage[x, y] = adjustedValue;
                }
            }

            // Return the adjusted image.
            return tempImage;
        }

        /*
         * createGaussianFilter: create a Gaussian filter of specific square size and with a specified sigma
         * input:   size                length and width of the Gaussian filter (only odd sizes)
         *          sigma               standard deviation of the Gaussian distribution
         * output:                      Gaussian filter
         */
        private double[,] CreateGaussianFilter(int size, double sigma)
        {
            // check if size is even, because we need odd sizes for Gaussian filters
            if (size % 2 == 0)
            {
                //size += 1; // Increase the size by 1 making it odd.
                //throw new ArgumentException("Only odd sizes are accepted.");
            }

            // initialize the 2D filter array
            double[,] filter = new double[size, size];

            // get half of the size for centering
            int halfSize = size / 2;

            // will store the total of filter values for normalization later
            double sum = 0.0;

            // loop over the 2D filter
            for (int x = -halfSize; x <= halfSize; x++)
            {
                for (int y = -halfSize; y <= halfSize; y++)
                {
                    // calculate the Gaussian value
                    double value = (1.0 / (2.0 * Math.PI * sigma * sigma)) *
                                   Math.Exp(-(x * x + y * y) / (2.0 * sigma * sigma));

                    // store it in the filter array
                    filter[x + halfSize, y + halfSize] = value;

                    // add to the sum
                    sum += value;
                }
            }

            // normalize the filter to make total 1
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    filter[i, j] /= sum;
                }
            }

            // give back the filter
            return filter;
        }


        /*
         * convolveImage: apply linear filtering of an input image
         * input:   inputImage          single-channel (byte) image
         *          filter              linear kernel
         * output:                      single-channel (byte) image
         */
        private byte[,] ConvolveImage(byte[,] srcImage, double[,] kernel)
        {
            // Getting the width and height of the input image.
            int width = srcImage.GetLength(0);
            int height = srcImage.GetLength(1);

            // Preparing an empty byte array to store the convolution results.
            byte[,] result = new byte[width, height];

            // Calculating the offset for kernel convolution.
            int foff = (kernel.GetLength(0) - 1) / 2;

            // Loop through the pixels of the image, but avoid the border pixels.
            for (int y = foff; y < height - foff; y++)
            {
                for (int x = foff; x < width - foff; x++)
                {
                    // Variable to store the result of convolution for each pixel.
                    double pixelValue = 0.0;

                    // Convolve with the kernel.
                    for (int fy = -foff; fy <= foff; fy++)
                    {
                        for (int fx = -foff; fx <= foff; fx++)
                        {
                            pixelValue += (double)(srcImage[x + fx, y + fy]) * kernel[fy + foff, fx + foff];
                        }
                    }

                    // Ensure pixel values are between 0 and 255.
                    if (pixelValue > 255) pixelValue = 255;
                    else if (pixelValue < 0) pixelValue = 0;

                    result[x, y] = (byte)pixelValue;
                }
            }

            // Handling the borders by copying the border pixels from the original image.
            for (int y = 0; y < foff; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result[x, y] = srcImage[x, y];
                    result[x, height - 1 - y] = srcImage[x, height - 1 - y];
                }
            }
            for (int x = 0; x < foff; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = srcImage[x, y];
                    result[width - 1 - x, y] = srcImage[width - 1 - x, y];
                }
            }

            return result;
        }

        private void GausFilt_Click(object sender, EventArgs e)
        {
            // Check if there's an image loaded in pictureBox2.
            if (pictureBox2.Image != null)
            {
                // Convert the image to grayscale bitmap.
                Bitmap grayscaleImageBitmap = new Bitmap(pictureBox2.Image);

                // Convert the bitmap to a 2D byte array.
                byte[,] grayscaleArray = BitmapToByteArray(grayscaleImageBitmap);

                // Set the size and sigma for Gaussian filter.
                //int kernelSize = (int)numericUpDown1.Value;

                k_size = (int)numericUpDown1.Value;
                Sigma = ((k_size - 1) / 4) + 0.5;   // Formula for sigma based on kernel size.

                // Create the Gaussian filter/kernel.
                double[,] gaussianKernel = CreateGaussianFilter(k_size, Sigma);

                // Apply convolution on the grayscale image using the Gaussian kernel.
                byte[,] convolvedArray = ConvolveImage(grayscaleArray, gaussianKernel);

                // Convert the convolved byte array back to bitmap for display.
                Bitmap convolvedBitmap = ByteToBitmap(convolvedArray);

                // Set the resulting image to pictureBox2.
                pictureBox2.Image = convolvedBitmap;
                pictureBox2.Refresh();
            }
        }

        private byte[,] BitmapToByteArray(Bitmap bmp)
        {
            // Convert the input bitmap to a 2D byte array.
            byte[,] result = new byte[bmp.Width, bmp.Height];

            // Loop through each pixel of the bitmap.
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    // Extract RGB components from each pixel and convert to grayscale value.
                    Color pixel = bmp.GetPixel(x, y);
                    result[x, y] = (byte)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);  // Grayscale conversion formula.
                }
            }
            return result;
        }

        private Bitmap ByteToBitmap(byte[,] byteArray)
        {
            // Convert a 2D byte array back to a bitmap.
            int width = byteArray.GetLength(0);
            int height = byteArray.GetLength(1);
            Bitmap result = new Bitmap(width, height);

            // Loop through each value in the byte array.
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Convert the grayscale byte value to RGB and set to the bitmap.
                    byte grayValue = byteArray[x, y];
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    result.SetPixel(x, y, grayColor);
                }
            }
            return result;
        }

        /*
         * medianFilter: apply median filtering on an input image with a kernel of specified size
         * input:   inputImage          single-channel (byte) image
         *          size                length/width of the median filter kernel
         * output:                      single-channel (byte) image
         */
        private byte[,] MedianFilter(byte[,] inputImage, int size)
        {
            // Ensure the filter size is odd, since median requires a central value.
            if (size % 2 == 0)
            {
                throw new ArgumentException("Only odd sizes are accepted.");
            }

            // Fetch the width and height of the input image.
            int width = inputImage.GetLength(0);
            int height = inputImage.GetLength(1);

            // Calculate the offset for median filtering. It determines the "radius" of the neighborhood.
            int offset = size / 2;

            // Prepare an empty byte array to store the result of the median filtering.
            byte[,] outputImage = new byte[width, height];

            // Loop through each pixel of the image.
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // A list to store the neighboring pixel values.
                    List<byte> neighbors = new List<byte>();

                    // Loop through each neighbor in the neighborhood defined by the filter size.
                    for (int i = -offset; i <= offset; i++)
                    {
                        for (int j = -offset; j <= offset; j++)
                        {
                            // Calculate the actual coordinates of the neighboring pixel.
                            int neighborX = x + i;
                            int neighborY = y + j;

                            // Ensure the neighboring coordinates are within image boundaries.
                            if (neighborX < 0) neighborX = 0;
                            if (neighborY < 0) neighborY = 0;
                            if (neighborX >= width) neighborX = width - 1;
                            if (neighborY >= height) neighborY = height - 1;

                            // Add the neighboring pixel's value to the list.
                            neighbors.Add(inputImage[neighborX, neighborY]);
                        }
                    }
                    // Sort the neighbors to find the median.
                    neighbors.Sort();

                    // Set the median value to the output image at the current pixel position.
                    outputImage[x, y] = neighbors[neighbors.Count / 2];
                }
            }

            return outputImage;  // Return the image after applying the median filter.
        }

        private void MedianFilt_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                Bitmap grayscaleImageBitmap = new Bitmap(pictureBox2.Image);

                // Convert the grayscale Bitmap to byte[,] format
                byte[,] grayscaleArray = BitmapToByteArray(grayscaleImageBitmap);

                // Get the median filter size. For this example, we'll use a size of 5. Adjust as needed.
                int filterSize = 5;

                // Apply the median filter
                byte[,] filteredArray = MedianFilter(grayscaleArray, filterSize);

                // Convert the filtered byte[,] array back to a Bitmap
                Bitmap filteredBitmap = ByteToBitmap(filteredArray);

                // Set the filtered Bitmap to pictureBox2
                pictureBox2.Image = filteredBitmap;
                pictureBox2.Refresh();
            }
        }

        /*
         * edgeMagnitude: calculate the image derivative of an input image and a provided edge kernel
         * input:   inputImage          single-channel (byte) image
         *          horizontalKernel    horizontal edge kernel
         *          virticalKernel      vertical edge kernel
         * output:                      single-channel (byte) image
         */
        private byte[,] EdgeMagnitude(byte[,] inputImage, sbyte[,] horizontalKernel, sbyte[,] verticalKernel)
        {
            int width = inputImage.GetLength(0);
            int height = inputImage.GetLength(1);
            byte[,] result = new byte[width, height];

            byte[,] Gx = ConvolveImage(inputImage, ConvertToDoubleArray(horizontalKernel));
            byte[,] Gy = ConvolveImage(inputImage, ConvertToDoubleArray(verticalKernel));

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double magnitude = Math.Sqrt(Gx[x, y] * Gx[x, y] + Gy[x, y] * Gy[x, y]);
                    result[x, y] = (byte)Math.Min(magnitude, 255);
                }
            }

            return result;
        }


        private double[,] ConvertToDoubleArray(sbyte[,] inputArray)
        {
            int width = inputArray.GetLength(0);
            int height = inputArray.GetLength(1);
            double[,] result = new double[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result[x, y] = inputArray[x, y];
                }
            }

            return result;
        }

        private void EdgeDed_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                Bitmap grayscaleImageBitmap = new Bitmap(pictureBox2.Image);

                // Convert the grayscale Bitmap to byte[,] format
                byte[,] grayscaleArray = BitmapToByteArray(grayscaleImageBitmap);

                // Define the horizontal and vertical Sobel operators
                sbyte[,] horizontalKernel = {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
        };

                sbyte[,] verticalKernel = {
            { -1, -2, -1 },
            {  0,  0,  0 },
            {  1,  2,  1 }
        };

                // Call the edgeMagnitude function
                byte[,] edgeMagnitudeArray = EdgeMagnitude(grayscaleArray, horizontalKernel, verticalKernel);

                // Convert the edgeMagnitude byte[,] array back to a Bitmap
                Bitmap edgeMagnitudeBitmap = ByteToBitmap(edgeMagnitudeArray);

                // Set the edgeMagnitude Bitmap to pictureBox2
                pictureBox2.Image = edgeMagnitudeBitmap;
                pictureBox2.Refresh();
            }
        }


        /*
         * thresholdImage: threshold a grayscale image
         * input:   inputImage          single-channel (byte) image
         * output:                      single-channel (byte) image with on/off values
         */
        private byte[,] ThresholdImage(byte[,] inputImage, byte threshold = 128)
        {
            int width = inputImage.GetLength(0);
            int height = inputImage.GetLength(1);
            byte[,] result = new byte[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result[x, y] = inputImage[x, y] >= threshold ? (byte)255 : (byte)0;
                }
            }

            return result;
        }

        private void Threshold_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)  // Assuming we want to threshold the image in pictureBox2
            {
                Bitmap imageBitmap = new Bitmap(pictureBox2.Image);

                // Convert the Bitmap to byte[,] format
                byte[,] imageArray = BitmapToByteArray(imageBitmap);

                // Call the thresholdImage function
                byte[,] thresholdedArray = ThresholdImage(imageArray);

                // Convert the thresholded byte[,] array back to a Bitmap
                Bitmap thresholdedBitmap = ByteToBitmap(thresholdedArray);

                // Set the thresholded Bitmap to pictureBox2 (or wherever we want to display the thresholded image)
                pictureBox2.Image = thresholdedBitmap;
                pictureBox2.Refresh();
            }
        }

        private void EdgeSharpinnig_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)  // Assuming we want to compute the histogram for the image in pictureBox2
            {
                Bitmap imageBitmap = new Bitmap(pictureBox2.Image);

                Bitmap sharpenedImage = new Bitmap(InputImage1.Width, InputImage1.Height);

                int[,] laplacianKernel = new int[,]
                {
                {  0, -1, 0 },
                { -1,  5, -1 },
                { 0, -1, 0 }
                };

                int kernelSize = 3;
                int kernelRadius = kernelSize / 2;

                for (int x = kernelRadius; x < InputImage1.Width - kernelRadius; x++)
                {
                    for (int y = kernelRadius; y < InputImage1.Height - kernelRadius; y++)
                    {
                        int newPixelValue = ApplyKernel(imageBitmap, x, y, laplacianKernel, kernelSize);
                        sharpenedImage.SetPixel(x, y, Color.FromArgb(newPixelValue, newPixelValue, newPixelValue));
                    }
                }
                // Set the thresholded Bitmap to pictureBox2 
                pictureBox2.Image = sharpenedImage;
                pictureBox2.Refresh();
            }
        }

        private static int ApplyKernel(Bitmap image, int x, int y, int[,] kernel, int kernelSize)
        {
            int kernelRadius = kernelSize / 2;
            int result = 0;

            for (int i = 0; i < kernelSize; i++)
            {
                for (int j = 0; j < kernelSize; j++)
                {
                    int pixelX = x - kernelRadius + i;
                    int pixelY = y - kernelRadius + j;

                    Color pixelColor = image.GetPixel(pixelX, pixelY);
                    int grayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114); // Convert to grayscale

                    result += grayValue * kernel[i, j];
                }
            }

            // Ensure the result is within the 0-255 range
            result = Math.Max(0, Math.Min(255, result));

            return result;
        }


        private void HistogramButton_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)  // Assuming we want to compute the histogram for the image in pictureBox2
            {
                Bitmap imageBitmap = new Bitmap(pictureBox2.Image);

                // Convert the Bitmap to byte[,] format
                byte[,] imageArray = BitmapToByteArray(imageBitmap);

                // Compute the histogram
                int[] histogram = ComputeHistogram(imageArray);

                // Create a bitmap to draw the histogram on
                Bitmap histogramBitmap = new Bitmap(256, 256); // Assuming a fixed size of 256x256 for simplicity
                DrawHistogramOnBitmap(histogram, histogramBitmap);

                // Display the histogram on pictureBox3
                pictureBox3.Image = histogramBitmap;
                pictureBox3.Refresh();
            }
        }


        private int[] ComputeHistogram(byte[,] imageArray)
        {
            int[] histogram = new int[256]; // For grayscale values from 0 to 255

            for (int x = 0; x < imageArray.GetLength(0); x++)
            {
                for (int y = 0; y < imageArray.GetLength(1); y++)
                {
                    int intensity = imageArray[x, y];
                    histogram[intensity]++;
                }
            }

            return histogram;
        }

        private void DrawHistogramOnBitmap(int[] histogram, Bitmap bitmap)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White); // Clear with white background

                int maxHistogramValue = histogram.Max();

                for (int i = 0; i < histogram.Length; i++)
                {
                    float heightRatio = (float)histogram[i] / maxHistogramValue;
                    float height = 256 * heightRatio;
                    g.DrawLine(Pens.Black, i, 256, i, 256 - height);
                }
            }
        }


        private void HistogramEqualization_Click(object sender, EventArgs e)
        {
            equalizedImage = null; // Clear the equalized image

            if (pictureBox2.Image != null)
            {
                Bitmap imageBitmap = new Bitmap(pictureBox2.Image);

                equalizedImage = EqualizeHistogram(imageBitmap);
                pictureBox2.Image = (Image)equalizedImage; // Display equalized image in pictureBox2
               
                equalizedHistogram = ComputeHistogram(equalizedImage); // Compute the histogram of the equalized image
                equalizedCDF = ComputeCDF(equalizedHistogram); // Compute the CDF of the equalized histogram

                DisplayCDF(equalizedCDF); // Display the CDF in pictureBox3
            }
        }

        private int[] ComputeHistogram(Bitmap image)
        {
            int[] histogram = new int[256]; // For grayscale values from 0 to 255

            int width = image.Width;
            int height = image.Height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114); // Convert to grayscale
                    histogram[grayValue]++;
                }
            }

            return histogram;
        }

        private Bitmap EqualizeHistogram(Bitmap inputImage)
        {
            int width = inputImage.Width;
            int height = inputImage.Height;
            Bitmap equalizedImage = new Bitmap(width, height);

            // Compute the histogram of the input image
            int[] histogram = ComputeHistogram(inputImage);

            // Compute the cumulative distribution function (CDF) of the histogram
            int[] cdf = ComputeCDF(histogram);

            // Total number of pixels in the image
            int totalPixels = width * height;

            // Equalize the image by mapping pixel intensities using the CDF
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = inputImage.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114); // Convert to grayscale

                    // Map the pixel intensity using the CDF
                    int newGrayValue = (int)(((double)cdf[grayValue] / totalPixels) * 255);

                    equalizedImage.SetPixel(x, y, Color.FromArgb(newGrayValue, newGrayValue, newGrayValue));
                }
            }

            return equalizedImage;
        }

        private int[] ComputeCDF(int[] histogram)
        {
            int[] cdf = new int[256];
            cdf[0] = histogram[0];

            for (int i = 1; i < 256; i++)
            {
                cdf[i] = cdf[i - 1] + histogram[i];
            }

            return cdf;
        }
        
        private void DisplayCDF(int[] equalizedCDF)
        {
            if (equalizedCDF != null)
            {
                int maxCDF = equalizedCDF[equalizedCDF.Length - 1];

                Bitmap cdfBitmap = new Bitmap(256, 256); // Create a new bitmap for CDF visualization

                for (int i = 0; i < equalizedCDF.Length; i++)
                {
                    int cdfValue = equalizedCDF[i];
                    int scaledCDF = (int)((double)cdfValue / maxCDF * 255); // Scale the CDF value

                    // Draw a vertical line for the CDF value
                    for (int y = 0; y <= scaledCDF; y++)
                    {
                        cdfBitmap.SetPixel(i, 255 - y, Color.Black); // Set a black pixel
                    }
                }

                pictureBox3.Image = (Image)cdfBitmap; // Display the CDF in pictureBox3
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void INFOIBV_Load(object sender, EventArgs e)
        {

        }

        private void Make_Image_A_button_Click(object sender, EventArgs e)
        {
            // First, convert the image to grayscale
            Conver_to_Gray_Click(sender, e);

            // Then, adjust its contrast
            Contrast_Adjustment_Click(sender, e);
        }

        private void Make_Image_B_Button_Click(object sender, EventArgs e)
        {
            // 1. Convert to Grayscale
            Conver_to_Gray_Click(sender, e);

            // 2. Adjust Contrast (This will result in Image A)
            Contrast_Adjustment_Click(sender, e); // Assuming this function adjusts the contrast

            // 3. Apply Gaussian filter with kernel size 5
            GausFilt_Click(sender, e);

            // 4. Edge Detection
            EdgeDed_Click(sender, e);

            // 5. Thresholding
            Threshold_Click(sender, e);
        }

        private void Make_Image_C_button_Click(object sender, EventArgs e)
        {
            // 1. Convert to Grayscale
            Conver_to_Gray_Click(sender, e);

            // 2. Adjust Contrast (This will result in Image A)
            Contrast_Adjustment_Click(sender, e); // Assuming this function adjusts the contrast

            // 3. Apply Median filter with kernel size 5
            MedianFilt_Click(sender, e);

            // 4. Edge Detection
            EdgeDed_Click(sender, e);

            // 5. Thresholding
            Threshold_Click(sender, e);
        }

        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 2 GO HERE ==============
        // ====================================================================

        //Structuring element
        private Bitmap createStructuringElement(string shape, int size)
        {
            Bitmap bmp = new Bitmap(size, size);

            if (shape == "Square")
            {
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        bmp.SetPixel(x, y, Color.Black); // 1
                    }
                }
            }
            else if (shape == "Plus")
            {
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        if (x == size / 2 || y == size / 2)
                        {
                            bmp.SetPixel(x, y, Color.Black); // 1
                        }
                        else
                        {
                            bmp.SetPixel(x, y, Color.White); // 0
                        }
                    }
                }
            }

            return bmp;
        }

        private void StructureElementBuildButton_Click(object sender, EventArgs e)
        {
            string shape = SetStrucElemShape.SelectedItem.ToString();
            int size = (int)StrucElemSize.Value;

            Bitmap result = createStructuringElement(shape, size);
//            pictureBox3.Image = result;

            // Print the structuring element to the console
            PrintStructuringElementToConsole(result);
        }

        private Bitmap ErodeImage(Bitmap source, Bitmap structElem)
        {
            Bitmap erodedImage = new Bitmap(source.Width, source.Height);

            bool isBinary = IsBinaryImage(source);

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    if (isBinary)
                    {
                        bool isEroded = false;

                        for (int seY = 0; seY < structElem.Height && !isEroded; seY++)
                        {
                            for (int seX = 0; seX < structElem.Width && !isEroded; seX++)
                            {
                                if (structElem.GetPixel(seX, seY).R == 0)
                                {
                                    int imageX = x + seX - structElem.Width / 2;
                                    int imageY = y + seY - structElem.Height / 2;

                                    if (imageX >= 0 && imageY >= 0 && imageX < source.Width && imageY < source.Height)
                                    {
                                        if (source.GetPixel(imageX, imageY).R == 0)
                                        {
                                            isEroded = true;
                                        }
                                    }
                                }
                            }
                        }

                        if (isEroded)
                        {
                            erodedImage.SetPixel(x, y, Color.Black);
                        }
                        else
                        {
                            erodedImage.SetPixel(x, y, Color.White);
                        }
                    }
                    else // Grayscale erosion
                    {
                        int minValue = 255;

                        for (int seY = 0; seY < structElem.Height; seY++)
                        {
                            for (int seX = 0; seX < structElem.Width; seX++)
                            {
                                if (structElem.GetPixel(seX, seY).R == 0)
                                {
                                    int imageX = x + seX - structElem.Width / 2;
                                    int imageY = y + seY - structElem.Height / 2;

                                    if (imageX >= 0 && imageY >= 0 && imageX < source.Width && imageY < source.Height)
                                    {
                                        int pixelValue = source.GetPixel(imageX, imageY).R;
                                        minValue = Math.Min(minValue, pixelValue);
                                    }
                                }
                            }
                        }

                        erodedImage.SetPixel(x, y, Color.FromArgb(minValue, minValue, minValue));
                    }
                }
            }

            return erodedImage;
        }

        private bool IsBinaryImage(Bitmap img)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color pixel = img.GetPixel(x, y);
                    if (pixel.R != 0 && pixel.R != 255)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private Bitmap DilateImage(Bitmap source, Bitmap structElem)
        {
            Bitmap dilatedImage = new Bitmap(source.Width, source.Height);
            bool isBinary = IsBinaryImage(source);

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    if (isBinary)
                    {
                        bool isDilated = false;

                        for (int seY = 0; seY < structElem.Height && !isDilated; seY++)
                        {
                            for (int seX = 0; seX < structElem.Width && !isDilated; seX++)
                            {
                                if (structElem.GetPixel(seX, seY).R == 0)
                                {
                                    int imageX = x + seX - structElem.Width / 2;
                                    int imageY = y + seY - structElem.Height / 2;

                                    if (imageX >= 0 && imageY >= 0 && imageX < source.Width && imageY < source.Height)
                                    {
                                        if (source.GetPixel(imageX, imageY).R == 255)
                                        {
                                            isDilated = true;
                                        }
                                    }
                                }
                            }
                        }

                        if (isDilated)
                        {
                            dilatedImage.SetPixel(x, y, Color.White);
                        }
                        else
                        {
                            dilatedImage.SetPixel(x, y, Color.Black);
                        }
                    }
                    else // Grayscale dilation
                    {
                        int maxValue = 0;

                        for (int seY = 0; seY < structElem.Height; seY++)
                        {
                            for (int seX = 0; seX < structElem.Width; seX++)
                            {
                                if (structElem.GetPixel(seX, seY).R == 0)
                                {
                                    int imageX = x + seX - structElem.Width / 2;
                                    int imageY = y + seY - structElem.Height / 2;

                                    if (imageX >= 0 && imageY >= 0 && imageX < source.Width && imageY < source.Height)
                                    {
                                        int pixelValue = source.GetPixel(imageX, imageY).R;
                                        maxValue = Math.Max(maxValue, pixelValue);
                                    }
                                }
                            }
                        }

                        dilatedImage.SetPixel(x, y, Color.FromArgb(maxValue, maxValue, maxValue));
                    }
                }
            }

            return dilatedImage;
        }

        private Bitmap OpenImage(Bitmap source, Bitmap structElem)
        {
            Bitmap temp = ErodeImage(source, structElem);
            return DilateImage(temp, structElem);
        }

        private Bitmap CloseImage(Bitmap source, Bitmap structElem)
        {
            Bitmap temp = DilateImage(source, structElem);
            return ErodeImage(temp, structElem);
        }

        private void DoSelectedFunction_Click(object sender, EventArgs e)
        {
            // Check if sturcture element, pictureBox2 or function is loaded/selected

            if (SetStrucElemShape.SelectedItem.ToString() == null || pictureBox2.Image == null || FunctionList.SelectedItem.ToString() == null)
            {
                MessageBox.Show("Check if the structure element is selected, Image2 is loaded, or the desired function is selected.");
                return;
            }

            // 1. Get the selected structuring element
            string shape = SetStrucElemShape.SelectedItem.ToString();
            int size = (int)StrucElemSize.Value;
            Bitmap structElem = createStructuringElement(shape, size);
            
            // 2. Get the selected operation
            string operation = FunctionList.SelectedItem.ToString();

            // 3. Apply the operation
            Bitmap sourceImage = (Bitmap)pictureBox2.Image.Clone();
            Bitmap resultImage;

            switch (operation)
            {
                case "Erode":
                    resultImage = ErodeImage(sourceImage, structElem);
                    break;       
                case "Dilate":
                    resultImage = DilateImage(sourceImage, structElem);
                    break;
                case "Open":
                    resultImage = OpenImage(sourceImage, structElem);
                    break;
                case "Close":
                    resultImage = CloseImage(sourceImage, structElem);
                    break;
                default:
                    resultImage = sourceImage;
                    break;
            }

            // 4. Display the result
                   
            pictureBox4.Image = resultImage;
            pictureBox4.Refresh();
            return;
        }

        private void PrintStructuringElementToConsole(Bitmap structElem)
        {
            for (int y = 0; y < structElem.Height; y++)
            {
                for (int x = 0; x < structElem.Width; x++)
                {
                    if (structElem.GetPixel(x, y).R == 0) // Black pixel
                    {
                        Console.Write("1");
                    }
                    else
                    {
                        Console.Write("0");
                    }
                }
                Console.WriteLine(); // Move to the next line after each row
            }
        }

        //and & or functions

        private void AND_BinaryImage_Click_1(object sender, EventArgs e)
        {
            // Check if both pictureBox1 and pictureBox2 have images
            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Both images must be loaded before performing the AND operation.");
                return;
            }

            // Convert the images in pictureBox1 and pictureBox2 to Bitmaps
            Bitmap image1 = new Bitmap(pictureBox1.Image);
            Bitmap image2 = new Bitmap(pictureBox2.Image);

            // Check if the dimensions of the two images match
            if (image1.Width != image2.Width || image1.Height != image2.Height)
            {
                MessageBox.Show("Both images must have the same dimensions to perform the AND operation.");
                return;
            }

            // Create a new Bitmap for the result
            Bitmap result = new Bitmap(image1.Width, image1.Height);

            // Perform the AND operation
            for (int y = 0; y < image1.Height; y++)
            {
                for (int x = 0; x < image1.Width; x++)
                {
                    Color pixel1 = image1.GetPixel(x, y);
                    Color pixel2 = image2.GetPixel(x, y);

                    if (pixel1.R == 255 && pixel2.R == 255)  // Both pixels are white
                    {
                        result.SetPixel(x, y, Color.White); // Set pixel
                    }
                    else
                    {
                        result.SetPixel(x, y, Color.Black); // Unset pixel
                    }
                }
            }

            // Display the result in pictureBox4
            pictureBox4.Image = result;
        }
        private void OR_BinaryImage_Click_1(object sender, EventArgs e)
        {
            // Check if both pictureBox1 and pictureBox2 have images
            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Both images must be loaded before performing the OR operation.");
                return;
            }

            // Convert the images in pictureBox1 and pictureBox2 to Bitmaps
            Bitmap image1 = new Bitmap(pictureBox1.Image);
            Bitmap image2 = new Bitmap(pictureBox2.Image);

            // Check if the dimensions of the two images match
            if (image1.Width != image2.Width || image1.Height != image2.Height)
            {
                MessageBox.Show("Both images must have the same dimensions to perform the OR operation.");
                return;
            }

            // Create a new Bitmap for the result
            Bitmap result = new Bitmap(image1.Width, image1.Height);

            // Perform the OR operation
            for (int y = 0; y < image1.Height; y++)
            {
                for (int x = 0; x < image1.Width; x++)
                {
                    Color pixel1 = image1.GetPixel(x, y);
                    Color pixel2 = image2.GetPixel(x, y);

                    if (pixel1.R == 255 || pixel2.R == 255)  // If either pixel is white
                    {
                        result.SetPixel(x, y, Color.White); // Set pixel
                    }
                    else
                    {
                        result.SetPixel(x, y, Color.Black); // Unset pixel
                    }
                }
            }

            // Display the result in pictureBox4
            pictureBox4.Image = result;
        }


        //count values
        private void ShowScrollableMessage(string message)
        {
            Form messageForm = new Form();
            messageForm.Width = 400;
            messageForm.Height = 600;
            messageForm.Text = "Histogram Values";

            TextBox messageTextBox = new TextBox();
            messageTextBox.Multiline = true;
            messageTextBox.ScrollBars = ScrollBars.Vertical;
            messageTextBox.Dock = DockStyle.Fill;
            messageTextBox.ReadOnly = true;
            messageTextBox.Text = message;

            messageForm.Controls.Add(messageTextBox);
            messageForm.ShowDialog();
        }

        private void CountValues_Click(object sender, EventArgs e)
        {
            
            Bitmap grayscaleImage = new Bitmap(pictureBox4.Image);

            // Compute the histogram of the grayscale image
            int[] histogram = ComputeHistogram(grayscaleImage);

            // Count the number of distinct values
            int distinctValuesCount = histogram.Count(value => value > 0);

            // Display the number of distinct values
            MessageBox.Show($"Number of distinct grayscale values: {distinctValuesCount}");

            // We want to display the occurrences of each distinct value:
            StringBuilder histogramValues = new StringBuilder();
            for (int i = 0; i < histogram.Length; i++)
            {
                if (histogram[i] > 0)
                {
                    histogramValues.AppendLine($"Value: {i}, Occurrences: {histogram[i]}");
                }
            }

            // Create a bitmap to draw the histogram on
            Bitmap histogramBitmap = new Bitmap(256, 256); // Assuming a fixed size of 256x256 for simplicity
            DrawHistogramOnBitmap(histogram, histogramBitmap);

            // Display the histogram on pictureBox3
            pictureBox3.Image = histogramBitmap;
            pictureBox3.Refresh();

            ShowScrollableMessage(histogramValues.ToString());
        }

        private void DrawBoundary(Bitmap image, List<(int, int)> boundary)
        {
            foreach (var point in boundary)
            {
                int x = point.Item1;
                int y = point.Item2;

                // Draw the main pixel
                image.SetPixel(x, y, Color.Red);

                // Draw surrounding pixels for thickness
                if (x > 0) image.SetPixel(x - 1, y, Color.Red);
                if (x < image.Width - 1) image.SetPixel(x + 1, y, Color.Red);
                if (y > 0) image.SetPixel(x, y - 1, Color.Red);
                if (y < image.Height - 1) image.SetPixel(x, y + 1, Color.Red);
            }
        }

        //floodfill
        private void Floodfill_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load a binary image in pictureBox1 first.");
                return;
            }

            Bitmap binaryImage = new Bitmap(pictureBox1.Image);
            int[,] labeledImage = FloodFill(binaryImage);
            Bitmap outputImage = LabeledArrayToBitmap(labeledImage);

            pictureBox1.Image = outputImage;
            pictureBox1.Refresh();
        }

        private int[,] FloodFill(Bitmap binaryImage)
        {
            int width = binaryImage.Width;
            int height = binaryImage.Height;
            int[,] labels = new int[width, height];
            int labelCounter = 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = binaryImage.GetPixel(x, y);
                    if (pixel.R == 255 && labels[x, y] == 0) // If it's a foreground pixel and not labeled yet
                    {
                        Queue<Point> queue = new Queue<Point>();
                        queue.Enqueue(new Point(x, y));

                        while (queue.Count > 0)
                        {
                            Point current = queue.Dequeue();
                            if (current.X >= 0 && current.Y >= 0 && current.X < width && current.Y < height && binaryImage.GetPixel(current.X, current.Y).R == 255 && labels[current.X, current.Y] == 0)
                            {
                                labels[current.X, current.Y] = labelCounter;

                                queue.Enqueue(new Point(current.X + 1, current.Y));
                                queue.Enqueue(new Point(current.X - 1, current.Y));
                                queue.Enqueue(new Point(current.X, current.Y + 1));
                                queue.Enqueue(new Point(current.X, current.Y - 1));
                            }
                        }

                        labelCounter++;
                    }
                }
            }

            return labels;
        }

        private Bitmap LabeledArrayToBitmap(int[,] labels)
        {
            int width = labels.GetLength(0);
            int height = labels.GetLength(1);
            Bitmap output = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int label = labels[x, y];
                    if (label == 0)
                    {
                        output.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        output.SetPixel(x, y, Color.FromArgb(label * 10 % 255, label * 20 % 255, label * 30 % 255)); // This will give different colors for different labels. Adjust as needed.
                    }
                }
            }

            return output;
        }

        //traceboundary
        private List<(int, int)> TraceBoundaryOuter(Bitmap image)
        {
            List<(int, int)> boundary = new List<(int, int)>();

            // Horizontal scan (left and right boundaries)
            for (int y = 0; y < image.Height; y++)
            {
                bool previousPixelWhite = false;

                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    bool currentPixelWhite = (pixel.R == 255 && pixel.G == 255 && pixel.B == 255);

                    if (currentPixelWhite && !previousPixelWhite) // Transition from black to white
                    {
                        boundary.Add((x, y));
                    }
                    else if (!currentPixelWhite && previousPixelWhite) // Transition from white to black
                    {
                        boundary.Add((x - 1, y));
                    }

                    previousPixelWhite = currentPixelWhite;
                }
            }

            // Vertical scan (top and bottom boundaries)
            for (int x = 0; x < image.Width; x++)
            {
                bool previousPixelWhite = false;

                for (int y = 0; y < image.Height; y++)
                {
                    Color pixel = image.GetPixel(x, y);
                    bool currentPixelWhite = (pixel.R == 255 && pixel.G == 255 && pixel.B == 255);

                    if (currentPixelWhite && !previousPixelWhite) // Transition from black to white
                    {
                        boundary.Add((x, y));
                    }
                    else if (!currentPixelWhite && previousPixelWhite) // Transition from white to black
                    {
                        boundary.Add((x, y - 1));
                    }

                    previousPixelWhite = currentPixelWhite;
                }
            }

            return boundary;
        }

        private void Real_traceBoundary_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image first.");
                return;
            }

            Bitmap inputBitmap = new Bitmap(pictureBox1.Image);
            List<(int, int)> boundary = TraceBoundaryOuter(inputBitmap);

            // For visualization: Draw the boundary on the image
            Bitmap boundaryImage = new Bitmap(inputBitmap);
            DrawBoundary(boundaryImage, boundary);
            pictureBox1.Image = boundaryImage;
            pictureBox1.Refresh();

            MessageBox.Show($"Boundary traced with {boundary.Count} points!");
        }

        //ExtractLargestShapeButton
        private void ExtractLargestShapeButton_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
            {
                MessageBox.Show("Please load a thresholded binary image in pictureBox2 first.");
                return;
            }

            Bitmap binaryImage = new Bitmap(pictureBox2.Image);
            Bitmap largestShapeImage = ExtractLargestShape(binaryImage);
            pictureBox4.Image = largestShapeImage;
            pictureBox4.Refresh();
        }
        
        private Bitmap ExtractLargestShape(Bitmap binaryImage)
        {
            // Step 1: Connected Components Labeling
            int currentLabel = 0;
            Dictionary<int, int> labelSizes = new Dictionary<int, int>();
            int[,] labels = new int[binaryImage.Width, binaryImage.Height];
            for (int y = 0; y < binaryImage.Height; y++)
            {
                for (int x = 0; x < binaryImage.Width; x++)
                {
                    if (binaryImage.GetPixel(x, y).R == 255 && labels[x, y] == 0) // If it's a white pixel and not yet labeled
                    {
                        currentLabel++;
                        int size = DFS(binaryImage, x, y, currentLabel, labels);
                        labelSizes[currentLabel] = size;
                    }
                }
            }

            // Step 2: Find the label with the maximum count
            int largestLabel = labelSizes.OrderByDescending(pair => pair.Value).First().Key;

            // Step 3: Extract Largest Shape
            Bitmap largestShape = new Bitmap(binaryImage.Width, binaryImage.Height);
            for (int y = 0; y < binaryImage.Height; y++)
            {
                for (int x = 0; x < binaryImage.Width; x++)
                {
                    largestShape.SetPixel(x, y, labels[x, y] == largestLabel ? Color.White : Color.Black);
                }
            }

            return largestShape;
        }

        private int DFS(Bitmap image, int x, int y, int label, int[,] labels)
        {
            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(x, y));
            int size = 0;

            while (stack.Count > 0)
            {
                Point current = stack.Pop();

                if (current.X < 0 || current.Y < 0 || current.X >= image.Width || current.Y >= image.Height || labels[current.X, current.Y] != 0 || image.GetPixel(current.X, current.Y).R != 255)
                    continue;

                labels[current.X, current.Y] = label;
                size++;

                stack.Push(new Point(current.X - 1, current.Y));
                stack.Push(new Point(current.X + 1, current.Y));
                stack.Push(new Point(current.X, current.Y - 1));
                stack.Push(new Point(current.X, current.Y + 1));
            }

            return size;
        }


        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 3 GO HERE ==============
        // ====================================================================

        // Event handler for the Hough Transform button
        private void Hough_Transform_Button_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image in pictureBox1.");
                return;
            }

            // Step 1: Convert the input image to grayscale
            Bitmap grayscaleImage = ConvertToGrayscale_((Bitmap)pictureBox1.Image);

            // Step 2: Apply edge detection (Sobel algorithm)
            Bitmap edgeImage = ApplySobelEdgeDetection_(grayscaleImage);

            // Step 3: Apply a threshold to obtain a binary image
            int threshold = Threshold_Hough.Value;
            Bitmap binaryImage = ApplyThreshold(edgeImage, threshold);

            // Step 4: Perform Hough Transform on the binary image
            Bitmap houghImage = HoughTransform(binaryImage);

            // Step 5: Display the Hough Transform result in pictureBox2
            pictureBox2.Image = houghImage; // Hough Transform result
            }

        private Bitmap ConvertToGrayscale_(Bitmap inputImage)
        {
            Bitmap grayscaleImage = new Bitmap(inputImage.Width, inputImage.Height);

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    Color pixel = inputImage.GetPixel(x, y);
                    int grayscaleValue = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                    grayscaleImage.SetPixel(x, y, Color.FromArgb(grayscaleValue, grayscaleValue, grayscaleValue));
                }
            }

            return grayscaleImage;
        }

        private Bitmap ApplySobelEdgeDetection_(Bitmap grayscaleImage)
        {
            int[,] sobelX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] sobelY = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            Bitmap edgeImage = new Bitmap(grayscaleImage.Width, grayscaleImage.Height);

            for (int y = 1; y < grayscaleImage.Height - 1; y++)
            {
                for (int x = 1; x < grayscaleImage.Width - 1; x++)
                {
                    int gx = 0;
                    int gy = 0;

                    for (int j = -1; j <= 1; j++)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            Color pixel = grayscaleImage.GetPixel(x + i, y + j);
                            int intensity = pixel.R;

                            gx += sobelX[j + 1, i + 1] * intensity;
                            gy += sobelY[j + 1, i + 1] * intensity;
                        }
                    }

                    int gradient = (int)Math.Sqrt(gx * gx + gy * gy);
                    int clampedGradient = Math.Min(255, gradient);
                    edgeImage.SetPixel(x, y, Color.FromArgb(clampedGradient, clampedGradient, clampedGradient));
                }
            }

            return edgeImage;
        }

        private void Threshold_Hough_Scroll(object sender, EventArgs e)
        {
            // Update the label text with the current threshold value
            Threshold_Label.Text = "MinIntensityThreshold:" + Threshold_Hough.Value;
        }

        private Bitmap ApplyThreshold(Bitmap inputImage, int threshold)
        {
            Bitmap binaryImage = new Bitmap(inputImage.Width, inputImage.Height);

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    Color pixel = inputImage.GetPixel(x, y);
                    int intensity = pixel.R;
                    Color resultColor = (intensity >= threshold) ? Color.White : Color.Black;
                    binaryImage.SetPixel(x, y, resultColor);
                }
            }

            return binaryImage;
        }

        private Bitmap HoughTransform(Bitmap image)
        {
            int h = image.Height;
            int w = image.Width;
            int maxRadius = (int)Math.Ceiling(Math.Sqrt(h * h + w * w));
            int thetaResolution = 500;  // 360 for 1 degree resolution, increase for finer resolution
            int[,] houghArray = new int[thetaResolution, 2 * maxRadius];

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    if (pixel.R == 255)  // Checking for white pixels
                    {
                        for (int theta = 0; theta < thetaResolution; theta++)
                        {
                            double thetaRadians = Math.PI * theta / thetaResolution;
                            double r = x * Math.Cos(thetaRadians) + y * Math.Sin(thetaRadians);
                            int rInt = (int)Math.Round(r) + maxRadius;  // Shifted to positive values
                            houghArray[theta, rInt]++;
                        }
                    }
                }
            }

            // Convert the Hough array to a Bitmap for visualization
            Bitmap houghBitmap = new Bitmap(thetaResolution, 2 * maxRadius + 100);
            int maxHoughValue = houghArray.Cast<int>().Max();

            for (int i = 0; i < thetaResolution; i++)
            {
                for (int j = 0; j < 2 * maxRadius; j++)
                {
                    double normalizedValue = (double)houghArray[i, j] / maxHoughValue;
                    int brightness = (int)(Math.Pow(normalizedValue, 0.5) * 255);  // Power-law transformation with gamma = 0.5
                    houghBitmap.SetPixel(i, j, Color.FromArgb(brightness, brightness, brightness));
                }
            }

            // Get threshold value from the TrackBar
            int threshold = Threshold_Hough.Value;

            // Thresholding the Hough array
            for (int i = 0; i < thetaResolution; i++)
            {
                for (int j = 0; j < 2 * maxRadius; j++)
                {
                    if (houghArray[i, j] < threshold)
                    {
                        houghBitmap.SetPixel(i, j, Color.Black);  // Set pixel to black if below threshold
                    }
                }
            }

            return houghBitmap;
        }


        // Function to perform peak finding
        private void Hough_Peak_Finding_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image in pictureBox1.");
                return;
            }

            if (!int.TryParse(Peak_Threshold.Text, out int peakThreshold))
            {
                MessageBox.Show("Please enter a valid peak threshold.");
                return;
            }

            // Step 1: Convert the input image to grayscale
            Bitmap grayscaleImage = ConvertToGrayscale_((Bitmap)pictureBox1.Image);

            // Step 2: Apply edge detection (Sobel algorithm)
            Bitmap edgeImage = ApplySobelEdgeDetection_(grayscaleImage);

            // Step 3: Apply a threshold to obtain a binary image
            int threshold = Threshold_Hough.Value;
            Bitmap binaryImage = ApplyThreshold(edgeImage, threshold);

            // Step 4: Perform Hough Transform on the binary image
            Bitmap houghImage = HoughTransform(binaryImage);

            // Step 5: Perform peak finding with non-maximum suppression
            //List<(int r, int theta)> 
            PeakList = PeakFinding(houghImage, peakThreshold);

            // Step 6: Display the list of (r, theta) pairs
            DisplayPeakList(PeakList);

            // Step 7: Visualize the peaks on a new image
            Bitmap peakImage = new Bitmap(houghImage.Width, houghImage.Height);
            using (Graphics g = Graphics.FromImage(peakImage))
            {
                g.Clear(Color.Black);  // set all pixels to black initially
                foreach (var (r, theta) in PeakList)
                {
                    g.FillEllipse(Brushes.White, r - 2, theta - 2, 4, 4);  // mark the peaks with white circles
                }
            }

            // Step 8: Display the peak image in pictureBox4
            pictureBox4.Image = peakImage;
            pictureBox4.Refresh();
        }


        private List<(int r, int theta)> PeakFinding(Bitmap houghImage, int peakThreshold)
        {
            int width = houghImage.Width;
            int height = houghImage.Height;

            List<(int r, int theta)> peaks = new List<(int r, int theta)>();

            for (int r = 0; r < width; r++)
            {
                for (int theta = 0; theta < height; theta++)
                {
                    Color pixelColor = houghImage.GetPixel(r, theta);
                    if (pixelColor.R >= peakThreshold)
                    {
                        bool isMaxima = true;

                        // Check for non-maximum suppression
                        for (int x = -1; x <= 1; x++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                int neighborR = r + x;
                                int neighborTheta = theta + y;

                                if (neighborR >= 0 && neighborR < width && neighborTheta >= 0 && neighborTheta < height)
                                {
                                    Color neighborColor = houghImage.GetPixel(neighborR, neighborTheta);
                                    if (neighborColor.R > pixelColor.R)
                                    {
                                        isMaxima = false;
                                        break;
                                    }
                                }
                            }

                            if (!isMaxima) break;
                        }

                        if (isMaxima)
                        {
                            peaks.Add((r, theta));
                        }
                    }
                }
            }

            return peaks;
        }

        private void DisplayPeakList(List<(int r, int theta)> PeakList)
        {
            // Display the list of (r, theta) pairs in the ListBox
            resultListBox.Items.Clear();

            foreach (var (r, theta) in PeakList)
            {
                resultListBox.Items.Add($"(r = {r}, theta = {theta})");
            }
            resultListBox.Refresh();
        }

        //Line detection
        private List<Point> FindHoughPeaks(Bitmap houghImage, double thresholdPercentage)
        {
            int width = houghImage.Width;
            int height = houghImage.Height;
            int[,] accumulatorArray = new int[width, height];


            // Convert houghImage to accumulatorArray
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixel = houghImage.GetPixel(x, y);
                    accumulatorArray[x, y] = (pixel.R + pixel.G + pixel.B) / 3;  // Assume grayscale
                }
            }

            // Thresholding
            int max = accumulatorArray.Cast<int>().Max();
            int threshold = (int)(max * thresholdPercentage);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (accumulatorArray[x, y] < threshold)
                    {
                        accumulatorArray[x, y] = 0;
                    }
                }
            }

            // Closing: Dilation followed by Erosion
            int[,] dilatedArray = Dilation(accumulatorArray, width, height);
            int[,] closedArray = Erosion(dilatedArray, width, height);

            // Region finding and finding centers to identify lines
            List<Point> peaks = FindRegionsAndCenters(closedArray, width, height);
            Console.WriteLine("Max Value: " + max);
            Console.WriteLine("Threshold: " + threshold);

            return peaks;
        }

        private int[,] Dilation(int[,] array, int width, int height)
        {
            int[,] result = new int[width, height];
            int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };

            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int maxValue = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        int nx = x + dx[i];
                        int ny = y + dy[i];
                        maxValue = Math.Max(maxValue, array[nx, ny]);
                    }
                    result[x, y] = maxValue;
                }
            }

            return result;
        }

        private int[,] Erosion(int[,] array, int width, int height)
        {
            int[,] result = new int[width, height];
            int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };

            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int minValue = int.MaxValue;
                    for (int i = 0; i < 8; i++)
                    {
                        int nx = x + dx[i];
                        int ny = y + dy[i];
                        minValue = Math.Min(minValue, array[nx, ny]);
                    }
                    result[x, y] = minValue;
                }
            }

            return result;
        }

        private List<Point> FindRegionsAndCenters(int[,] array, int width, int height)
        {
            List<Point> centers = new List<Point>();
            bool[,] visited = new bool[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (array[x, y] > 0 && !visited[x, y])
                    {
                        Queue<Point> queue = new Queue<Point>();
                        queue.Enqueue(new Point(x, y));
                        visited[x, y] = true;

                        int sumX = 0, sumY = 0, count = 0;

                        while (queue.Count > 0)
                        {
                            Point point = queue.Dequeue();
                            sumX += point.X;
                            sumY += point.Y;
                            count++;

                            for (int dx = -1; dx <= 1; dx++)
                            {
                                for (int dy = -1; dy <= 1; dy++)
                                {
                                    int nx = point.X + dx;
                                    int ny = point.Y + dy;
                                    if (nx >= 0 && nx < width && ny >= 0 && ny < height && array[nx, ny] > 0 && !visited[nx, ny])
                                    {
                                        queue.Enqueue(new Point(nx, ny));
                                        visited[nx, ny] = true;
                                    }
                                }
                            }
                        }

                        int centerX = sumX / count;
                        int centerY = sumY / count;
                        centers.Add(new Point(centerX, centerY));
                    }
                }
            }

            return centers;
        }
        private void Hough_Line_Finding_Button_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image in pictureBox1.");
                return;
            }

            Bitmap originalImage = new Bitmap(pictureBox1.Image);
            List<Point> peaks = FindHoughPeaks(new Bitmap(pictureBox2.Image), 0.7);  // Assuming 70% threshold

            if (peaks != null && peaks.Any())
            {
                // Create a new bitmap for drawing the lines
                Bitmap lineImage = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);

                using (Graphics g = Graphics.FromImage(lineImage))
                {
                    g.Clear(Color.Black);  // Set all pixels to black initially

                    foreach (var peak in peaks)
                    {
                        int r = peak.Y - originalImage.Width;  // Adjust the r value to account for the earlier shift
                        int theta = peak.X * 180 / 500;  // Convert theta from pixel coordinate to degree, assuming 500-pixel width represents 180 degrees

                        double thetaRadians = theta * Math.PI / 180;
                        int yOffset = 200;  // Adjust this value to shift the lines vertically
                        for (int x = 0; x < originalImage.Width; x++)
                        {
                            int y = (int)((r - x * Math.Cos(thetaRadians)) / Math.Sin(thetaRadians)) - yOffset;
                            if (y >= 0 && y < originalImage.Height)
                            {
                                lineImage.SetPixel(x, y, Color.Red);  // Draw the line in red
                            }
                        }
                    }
                }

                // Display the lines image in pictureBox4
                pictureBox4.Image = lineImage;
                pictureBox4.Refresh();
            }
            else
            {
                MessageBox.Show("Peaks list is empty or null. Check your data source.");
            }
        }
       
        private void Min_Length_Scroll(object sender, EventArgs e)
        {
            // Update the label text with the current length value
            Min_Length_Label.Text = "MinLength:" + Min_Length.Value;
        }

        private void Max_Gap_Scroll(object sender, EventArgs e)
        {
            // Update the label text with the current gap value
            MaxGap_Label.Text = "MaxGap:" + Max_Gap.Value;
        }

        //Hough visualization
        private void Hough_Visualization_Button_Click(object sender, EventArgs e)
        {
            // Check if an image is loaded in pictureBox1
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image in pictureBox1.");
                return;
            }

            Bitmap originalImage = new Bitmap(pictureBox1.Image);
            List<Point> peaks = FindHoughPeaks(new Bitmap(pictureBox2.Image), 0.7);  // Assuming 70% threshold

            List<(Point start, Point end)> lineSegments = ConvertPeaksToSegments(peaks, originalImage);

            // Visualize and display the line segments in pictureBox1
            VisualizeHoughLineSegments(lineSegments);
        }

        private List<(Point start, Point end)> ConvertPeaksToSegments(List<Point> peaks, Bitmap originalImage)
        {
            List<(Point start, Point end)> lineSegments = new List<(Point start, Point end)>();

            // Convert peaks to line segments...
            // This is a simplified example. You might need to adapt this logic to match your requirements.

            foreach (var peak in peaks)
            {
                int r = peak.Y - originalImage.Width;  // Adjust the r value to account for the earlier shift
                int theta = peak.X * 180 / 500;  // Convert theta from pixel coordinate to degree, assuming 500-pixel width represents 180 degrees

                double thetaRadians = theta * Math.PI / 180;

                // Calculate the two endpoints of the line segment
                Point p1, p2;
                if (theta == 0 || theta == 180)  // Vertical line
                {
                    p1 = new Point(r, 0);
                    p2 = new Point(r, originalImage.Height);
                }
                else if (theta == 90)  // Horizontal line
                {
                    p1 = new Point(0, r);
                    p2 = new Point(originalImage.Width, r);
                }
                else  // General case
                {
                    int x1 = 0;
                    int y1 = (int)((r - x1 * Math.Cos(thetaRadians)) / Math.Sin(thetaRadians));

                    int x2 = originalImage.Width;
                    int y2 = (int)((r - x2 * Math.Cos(thetaRadians)) / Math.Sin(thetaRadians));

                    p1 = new Point(x1, y1);
                    p2 = new Point(x2, y2);
                }

                lineSegments.Add((p1, p2));
            }

            return lineSegments;
        }


        private void VisualizeHoughLineSegments(List<(Point start, Point end)> lineSegments)
        {
            // Create a copy of the original image from pictureBox1
            Bitmap originalImage = new Bitmap(pictureBox1.Image);

            using (Graphics g = Graphics.FromImage(originalImage))
            {
                // Define the color and pen for drawing line segments (e.g., red)
                Pen linePen = new Pen(Color.Red);

                // Define a hard-coded vertical offset
                int yOffset = 200;  // Adjust this value to shift the lines vertically

                foreach (var segment in lineSegments)
                {
                    // Adjust the y-coordinates of the start and end points
                    Point adjustedStart = new Point(segment.start.X, segment.start.Y - yOffset);
                    Point adjustedEnd = new Point(segment.end.X, segment.end.Y - yOffset);

                    g.DrawLine(linePen, adjustedStart, adjustedEnd);
                }
            }

            // Display the visualized image in pictureBox1
            pictureBox1.Image = originalImage;
            pictureBox1.Refresh();  // Refresh pictureBox1 to show the updated image
        }
        //circles 
        
        private Bitmap HoughTransformCircles(Bitmap image, int minRadius, int maxRadius)
        {
            int h = image.Height;
            int w = image.Width;
            int[,,] houghArray = new int[w, h, maxRadius - minRadius + 1];

            // Voting
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    if (pixel.R == 255)  // Assuming white pixels represent edges
                    {
                        for (int radius = minRadius; radius <= maxRadius; radius++)
                        {
                            for (int theta = 0; theta < 360; theta++)
                            {
                                double thetaRadians = Math.PI * theta / 180;
                                int a = x - (int)(radius * Math.Cos(thetaRadians));
                                int b = y - (int)(radius * Math.Sin(thetaRadians));
                                if (a >= 0 && a < w && b >= 0 && b < h)
                                {
                                    houghArray[a, b, radius - minRadius]++;
                                }
                            }
                        }
                    }
                }
            }

            // Finding max votes
            int maxVotes = 0;
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    for (int r = 0; r < maxRadius - minRadius + 1; r++)
                    {
                        maxVotes = Math.Max(maxVotes, houghArray[x, y, r]);
                    }
                }
            }

            // Thresholding (e.g., at 50% of max votes)
            int threshold = maxVotes / 2;
            Bitmap circleImage = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(circleImage))
            {
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        for (int r = 0; r < maxRadius - minRadius + 1; r++)
                        {
                            if (houghArray[x, y, r] >= threshold)
                            {
                                g.DrawEllipse(Pens.Red, x - r + minRadius, y - r + minRadius, 1 * (r + minRadius), 1 * (r + minRadius));
                            }
                        }
                    }
                }
            }

            return circleImage;
        }

        private void Forcircles_Click_1(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load an image in pictureBox1 first.");
                return;
            }

            Bitmap inputImage = new Bitmap(pictureBox1.Image);
            Bitmap circleImage = HoughTransformCircles(inputImage, 10, 50);  // Assuming radius range 10 to 50
            pictureBox2.Image = circleImage;
            pictureBox2.Refresh();
        }

        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 4 GO HERE ==============
        // ====================================================================
             
        private async void PreprocessButton_Click(object sender, EventArgs e)
        {
            Conver_to_Gray_Click(sender, e);
            GausFilt_Click(sender, e);
            HistogramEqualization_Click(sender, e);
            EdgeDed_Click(sender, e);

        }

        private void ContourDetection_Click(object sender, EventArgs e)
        {
            // Get the preprocessed image from pictureBox1
            Bitmap preprocessedImage = (Bitmap)pictureBox1.Image;

            // Create a new bitmap to draw the contours on
            Bitmap contourImage = new Bitmap(preprocessedImage.Width, preprocessedImage.Height);

            Random random = new Random();

            // Create a byte array to hold pixel data
            byte[] pixels = BitmapToByteArrayContour(preprocessedImage);

            // Iterate through the pixels of the preprocessed image to find starting points for contours
            for (int y = 0; y < preprocessedImage.Height; y++)
            {
                for (int x = 0; x < preprocessedImage.Width; x++)
                {
                    // Assuming white pixels represent edges
                    if (IsEdgePixel(pixels, x, y, preprocessedImage.Width))
                    {
                        // Trace contour from the current starting point
                        List<Point> contour = TraceContour(pixels, new Point(x, y), preprocessedImage.Width);

                        // Mark the traced pixels so they don't initiate another contour trace
                        MarkTracedPixels(pixels, contour, preprocessedImage.Width);

                        // Generate a random color
                        Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                        // Draw the traced contour on the contourImage
                        DrawContour(contourImage, contour, randomColor);
                    }
                }
            }

            // Display the contour image in pictureBox2
            pictureBox2.Image = contourImage;
            pictureBox2.Refresh();
        }
        private void MarkTracedPixels(byte[] pixels, List<Point> contour, int width)
        {
            foreach (Point point in contour)
            {
                int index = (point.Y * width + point.X) * 4;  // Assuming a 32-bit image (4 bytes per pixel)
                pixels[index] = 0;      // Set Red to 0
                pixels[index + 1] = 0;  // Set Green to 0
                pixels[index + 2] = 0;  // Set Blue to 0
            }
        }
        private void DrawContour(Bitmap image, List<Point> contour, Color color)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                if (contour.Count > 1)
                {
                    Pen myPen = new Pen(Color.Red);  // Always use a red pen
                    g.DrawPolygon(myPen, contour.ToArray());
                }
            }
        }

        private const int MaxIterations = 20000;  // Set a reasonable maximum to prevent infinite loops

        private List<Point> TraceContour(byte[] pixels, Point start, int width)
        {
            List<Point> contour = new List<Point>();
            Point current = start;
            Point previous = new Point(start.X - 1, start.Y);  // Assume we start by coming from the left

            try
            {
                int iterationCount = 0;  // Initialize iteration count

                do
                {
                    contour.Add(current);
                    Point next = GetNextNeighbor(pixels, current, previous, width);

                    if (next == Point.Empty) break;  // No more neighbors to visit

                    previous = current;
                    current = next;

                    iterationCount++;  // Increment iteration count

                    // Check for max iterations to prevent potential infinite loop
                    if (iterationCount > MaxIterations)
                    {
                        Debug.WriteLine("Max iterations reached, exiting loop");
                        break;
                    }

                } while (current != start);  // Stop when we come full circle
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // Optionally re-throw the exception or handle it in some way
            }

            return contour;
        }


        private Point GetNextNeighbor(byte[] pixels, Point current, Point previous, int width)
        {
            Point[] directions =
            {
        new Point(-1, 0), new Point(0, -1),
        new Point(1, 0), new Point(0, 1)
    };  // Left, Up, Right, Down

            foreach (Point dir in directions)
            {
                Point neighbor = new Point(current.X + dir.X, current.Y + dir.Y);
                if (IsWithinBounds(neighbor, width, pixels.Length / (4 * width)) && IsEdgePixel(pixels, neighbor.X, neighbor.Y, width) && neighbor != previous)
                {
                    return neighbor;
                }
            }
            return Point.Empty;  // No unvisited neighbors
        }

        private bool IsWithinBounds(Point point, int width, int height)
        {
            return point.X >= 0 && point.X < width && point.Y >= 0 && point.Y < height;
        }

        private byte[] BitmapToByteArrayContour(Bitmap bitmap)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, rgbValues, 0, bytes);

            bitmap.UnlockBits(bmpData);

            return rgbValues;
        }

        private bool IsEdgePixel(byte[] pixels, int x, int y, int width)
        {
            int index = (y * width + x) * 4;  // Assuming a 32-bit image (4 bytes per pixel)
            return pixels[index] > 200 && pixels[index + 1] > 200 && pixels[index + 2] > 200
                   && !(pixels[index] == 0 && pixels[index + 1] == 0 && pixels[index + 2] == 0);  // Ignore black pixels
        }


    }

}