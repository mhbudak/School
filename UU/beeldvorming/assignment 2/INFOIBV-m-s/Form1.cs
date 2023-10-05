using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace INFOIBV
{
    public partial class INFOIBV : Form
    {
        private Bitmap InputImage;
        private Bitmap OutputImage;
        private int k_size;
        private double Sigma;
        private Bitmap inputImage;
        private Bitmap equalizedImage;
        private int[] equalizedHistogram;
        private int[] equalizedCDF;
        private int[,] structuringElement; // Declare it at the class level

        public INFOIBV()
        {
            InitializeComponent();
        }

        /*
         * loadImageButton1_Click: process when user clicks "Load Image-1" button
         */
        private void LoadImageButton1_Click(object sender, EventArgs e)
        {
           if (openImageDialog.ShowDialog() == DialogResult.OK)             // open file dialog
            {
                string file = openImageDialog.FileName;                     // get the file name
                ImageFileName1.Text = file;                                  // show file name
                InputImage?.Dispose();                                      // reset image

                InputImage = new Bitmap(file);                              // create new Bitmap from file
                if (InputImage.Size.Height <= 0 || InputImage.Size.Width <= 0 ||
                    InputImage.Size.Height > 512 || InputImage.Size.Width > 512) // dimension check (may be removed or altered)
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                    pictureBox1.Image = (Image) InputImage;                 // display input image
                    Image1.Visible = false;
            }
        }

        /*
         * loadImageButton2_Click: process when user clicks "Load Image-2" button
         */
        private void LoadImage2_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)             // open file dialog
            {
                string file = openImageDialog.FileName;                     // get the file name
                ImageFileName2.Text = file;                                  // show file name
                InputImage?.Dispose();                                      // reset image

                InputImage = new Bitmap(file);                              // create new Bitmap from file
                if (InputImage.Size.Height <= 0 || InputImage.Size.Width <= 0 ||
                    InputImage.Size.Height > 512 || InputImage.Size.Width > 512) // dimension check (may be removed or altered)
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                    pictureBox2.Image = (Image)InputImage;                 // display input image
                    Image2.Visible = false;

            }
        }

        /*
        * Conver_to_Gray_Click: process when user clicks "Convert to Gray" button
        */
        private void Conver_to_Gray_Click(object sender, EventArgs e)
        {
            
            if (InputImage == null) return;                                 // get out if no input image
            OutputImage?.Dispose();                                         // reset output image
            OutputImage = new Bitmap(InputImage.Size.Width, InputImage.Size.Height); // create new output image
            Color[,] ourImage = new Color[InputImage.Size.Width, InputImage.Size.Height]; // create array to speed-up operations (Bitmap functions are very slow)

            // copy input Bitmap to array            
            for (int x = 0; x < InputImage.Size.Width; x++)                 // loop over columns
            {
                for (int y = 0; y < InputImage.Size.Height; y++)            // loop over rows
                {
                    ourImage[x, y] = InputImage.GetPixel(x, y);              // set pixel color in array at (x,y)
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
            progressBar.Maximum = InputImage.Size.Width * InputImage.Size.Height;
            progressBar.Value = 1;
            progressBar.Step = 1;


            // process all pixels in the image
            for (int x = 0; x < InputImage.Size.Width; x++)                 // loop over columns
                for (int y = 0; y < InputImage.Size.Height; y++)            // loop over rows
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

        /*
         * saveButton_Click: process when user clicks "Save" button
         */
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null) return;                                // get out if no output image
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                pictureBox2.Image.Save(saveImageDialog.FileName);                 // save the output image 
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
         * input:   inputImage          single-channel (byte) image
         * output:                      single-channel (byte) image
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
         * input:   inputImage          single-channel (byte) image
         * output:                      single-channel (byte) image
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
        private byte[,] ThresholdImage(byte[,] inputImage, byte threshold = 100)
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
            if (pictureBox2.Image != null)  // Assuming you want to threshold the image in pictureBox2
            {
                Bitmap imageBitmap = new Bitmap(pictureBox2.Image);

                // Convert the Bitmap to byte[,] format
                byte[,] imageArray = BitmapToByteArray(imageBitmap);

                // Call the thresholdImage function
                byte[,] thresholdedArray = ThresholdImage(imageArray);

                // Convert the thresholded byte[,] array back to a Bitmap
                Bitmap thresholdedBitmap = ByteToBitmap(thresholdedArray);

                // Set the thresholded Bitmap to pictureBox2 (or wherever you want to display the thresholded image)
                pictureBox2.Image = thresholdedBitmap;
                pictureBox2.Refresh();
            }
        }
        private void EdgeSharpinnig_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)  // Assuming we want to compute the histogram for the image in pictureBox2
            {
                Bitmap imageBitmap = new Bitmap(pictureBox2.Image);

                Bitmap sharpenedImage = new Bitmap(InputImage.Width, InputImage.Height);

                int[,] laplacianKernel = new int[,]
                {
                {  0, -1, 0 },
                { -1,  5, -1 },
                { 0, -1, 0 }
                };

                int kernelSize = 3;
                int kernelRadius = kernelSize / 2;

                for (int x = kernelRadius; x < InputImage.Width - kernelRadius; x++)
                {
                    for (int y = kernelRadius; y < InputImage.Height - kernelRadius; y++)
                    {
                        int newPixelValue = ApplyKernel(imageBitmap, x, y, laplacianKernel, kernelSize);
                        sharpenedImage.SetPixel(x, y, Color.FromArgb(newPixelValue, newPixelValue, newPixelValue));
                    }
                }
                // Set the thresholded Bitmap to pictureBox2 (or wherever you want to display the thresholded image)
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
            if (pictureBox2.Image != null)  // Assuming you want to compute the histogram for the image in pictureBox2
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

        private void StructureElementBuildButton_Click(object sender, EventArgs e)
        {
            // Check if an item is selected in the ListBox
            if (SetStrucElemShape.SelectedItem != null)
            {
                // Get the selected shape from the ListBox
                string selectedShape = SetStrucElemShape.SelectedItem.ToString();

                // Get the selected size from the DomainUpDown control
                int selectedSize = Convert.ToInt32(StrucElemSize.Text);

                // Create the structuring element based on shape and size
                int[,] structuringElement = StructuringElementBuild(selectedShape, selectedSize);

                // Assign the computed structuringElement to the class-level variable
                this.structuringElement = structuringElement;

                // Visualize and display the structuring element
                VisualizeStructuringElement(structuringElement);
            }
            else
            {
                // Handle the case where no item is selected in the ListBox (e.g., display an error message)
                MessageBox.Show("Please select a shape from the list.");
            }
        }


        private int[,] StructuringElementBuild(string shape, int size)
        {
            int elementSize = size;
            int[,] structuringElement;
            
            //DomainUpDown strucElemSize = StrucElemSize; // Initialize the control


            if (shape.ToLower() == "plus")
            {
                int plusSize = 3;
                int[,] basePlusElement = CreatePlusElement(plusSize);

                // Initialize the structuring element with the base plus element
                structuringElement = basePlusElement;

                // Perform iterative dilation to obtain the desired size
                while (elementSize > plusSize)
                {
                    int newElementSize = plusSize + 2;
                    int[,] newPlusElement = CreatePlusElement(newElementSize);

                    // Create a new structuring element with the updated size
                    int[,] newStructuringElement = new int[newElementSize, newElementSize];

                    // Copy the values from the current structuring element to the new one
                    for (int i = 0; i < plusSize; i++)
                    {
                        for (int j = 0; j < plusSize; j++)
                        {
                            newStructuringElement[i + 1, j + 1] = structuringElement[i, j];
                        }
                    }

                    // Merge the current structuring element with the new plus element
                    for (int i = 0; i < newElementSize; i++)
                    {
                        for (int j = 0; j < newElementSize; j++)
                        {
                            int currentValue = newStructuringElement[i, j];
                            int newValue = currentValue | newPlusElement[i, j];
                            newStructuringElement[i, j] = newValue;
                        }
                    }

                    // Update the structuring element and size for the next iteration
                    structuringElement = newStructuringElement;
                    plusSize = newElementSize;
                }
            }
            else if (shape.ToLower() == "square")
            {
                // Create a square-shaped structuring element
                structuringElement = new int[elementSize, elementSize];
                for (int i = 0; i < elementSize; i++)
                {
                    for (int j = 0; j < elementSize; j++)
                    {
                        structuringElement[i, j] = 1;
                    }
                }
            }
            else
            {
                throw new ArgumentException("Invalid shape. Supported shapes are 'plus' and 'square'.");
            }

            return structuringElement;
        }

        private int[,] CreatePlusElement(int size)
        {
            int plusSize = size;
            int[,] plusElement = new int[plusSize, plusSize];

            // Create a plus-shaped structuring element
            for (int i = 0; i < plusSize; i++)
            {
                for (int j = 0; j < plusSize; j++)
                {
                    if (i == plusSize / 2 || j == plusSize / 2)
                    {
                        plusElement[i, j] = 1;
                    }
                    else
                    {
                        plusElement[i, j] = 0;
                    }
                }
            }

            return plusElement;
        }

        private void VisualizeStructuringElement(int[,] structuringElement)
        {
            int seWidth = structuringElement.GetLength(0);
            int seHeight = structuringElement.GetLength(1);

            // Create a bitmap to draw the structuring element
            Bitmap seBitmap = new Bitmap(seWidth * 20, seHeight * 20); // Adjust the scale as needed

            using (Graphics g = Graphics.FromImage(seBitmap))
            {
                g.Clear(Color.Black); // Clear with a white background

                // Calculate the size of each cell for visualization
                int cellWidth = seBitmap.Width / seWidth;
                int cellHeight = seBitmap.Height / seHeight;

                // Draw the structuring element
                for (int x = 0; x < seWidth; x++)
                {
                    for (int y = 0; y < seHeight; y++)
                    {
                        if (structuringElement[x, y] != 0)
                        {
                            // Draw a filled rectangle for each non-zero element
                            g.FillRectangle(Brushes.White, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                    }
                }
            }

            // Display the structuring element in pictureBox3
            pictureBox3.Image = seBitmap;
            pictureBox3.Refresh();
        }

        private void DoSelectedFunction_Click(object sender, EventArgs e)
        {
            // Determine the selected function from the ListBox
            string selectedFunction;

            selectedFunction = (string)FunctionList.SelectedItem;

            //if (!string.IsNullOrEmpty(selectedFunction)) 
            //{
            //    selectedFunction= "Erode";
            //}

            if ((pictureBox2.Image != null) && (structuringElement != null))
            {
                Bitmap inputImageBitmap = new Bitmap(pictureBox2.Image);


                if (IsBinaryImage(inputImageBitmap))
                {
                    // Binary Image
                    bool[,] binaryImageArray = BitmapToBinaryArray(inputImageBitmap);

                    switch (selectedFunction)
                    {
                        case "Erode":
                            pictureBox2.Image = BinaryArrayToBitmap(ErodeBinaryImage(binaryImageArray, structuringElement));
                            break;

                        case "Dilate":
                            pictureBox2.Image = BinaryArrayToBitmap(DilateBinaryImage(binaryImageArray, structuringElement));
                            break;

                        case "Open":
                            pictureBox2.Image = BinaryArrayToBitmap(OpenBinaryImage(binaryImageArray, structuringElement));
                            break;

                        case "Close":
                            pictureBox2.Image = BinaryArrayToBitmap(CloseBinaryImage(binaryImageArray, structuringElement));
                            break;

                        default:
                            // Handle unsupported function
                            break;
                    }
                }
                else
                {
                    // Grayscale Image
                    byte[,] grayscaleImageArray = BitmapToByteArray(inputImageBitmap);

                    switch (selectedFunction)
                    {
                        case "Erode":
                            pictureBox2.Image = ByteToBitmap(ErodeGrayscaleImage(grayscaleImageArray, structuringElement));
                            break;

                        case "Dilate":
                            pictureBox2.Image = ByteToBitmap(DilateGrayscaleImage(grayscaleImageArray, structuringElement));
                            break;

                        case "Open":
                            pictureBox2.Image = ByteToBitmap(OpenGrayscaleImage(grayscaleImageArray, structuringElement));
                            break;

                        case "Close":
                            pictureBox2.Image = ByteToBitmap(CloseGrayscaleImage(grayscaleImageArray, structuringElement));
                            break;

                        default:
                            // Handle unsupported function
                            break;
                    }
                }

                pictureBox2.Refresh();
            }
            else
            {
                // Handle the case where no item is selected in the ListBox (e.g., display an error message)
                MessageBox.Show("Please load an image and use some functions to get another image or build a structring element or select a function from list");
            }
        }


        private Bitmap BinaryArrayToBitmap(bool[,] binaryArray)
        {
            int width = binaryArray.GetLength(0);
            int height = binaryArray.GetLength(1);
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = binaryArray[x, y] ? Color.Black : Color.White;
                    bitmap.SetPixel(x, y, pixelColor);
                }
            }

            return bitmap;
        }

        private bool[,] BitmapToBinaryArray(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            bool[,] binaryArray = new bool[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    binaryArray[x, y] = pixelColor.GetBrightness() < 0.5f; // Threshold to consider black or white
                }
            }

            return binaryArray;
        }

        // Grayscale Erosion
        private byte[,] ErodeGrayscaleImage(byte[,] inputImage, int[,] structuringElement)
        {
            int imageWidth = inputImage.GetLength(0);
            int imageHeight = inputImage.GetLength(1);
            int seWidth = structuringElement.GetLength(0);
            int seHeight = structuringElement.GetLength(1);
            int seOffsetX = seWidth / 2;
            int seOffsetY = seHeight / 2;

            byte[,] erodedImage = new byte[imageWidth, imageHeight];

            for (int x = 0; x < imageWidth; x++)
            {
                for (int y = 0; y < imageHeight; y++)
                {
                    byte minValue = 255; // Initialize with the maximum possible value
                    for (int i = -seOffsetX; i <= seOffsetX; i++)
                    {
                        for (int j = -seOffsetY; j <= seOffsetY; j++)
                        {
                            int pixelX = x + i;
                            int pixelY = y + j;

                            if (pixelX >= 0 && pixelX < imageWidth && pixelY >= 0 && pixelY < imageHeight)
                            {
                                byte pixelValue = inputImage[pixelX, pixelY];
                                int seValue = structuringElement[i + seOffsetX, j + seOffsetY];
                                byte erodedValue = (byte)(pixelValue - seValue);
                                if (erodedValue < minValue)
                                {
                                    minValue = erodedValue;
                                }
                            }
                        }
                    }
                    erodedImage[x, y] = minValue;
                }
            }

            return erodedImage;
        }

        // Grayscale Dilation
        private byte[,] DilateGrayscaleImage(byte[,] inputImage, int[,] structuringElement)
        {
            int imageWidth = inputImage.GetLength(0);
            int imageHeight = inputImage.GetLength(1);
            int seWidth = structuringElement.GetLength(0);
            int seHeight = structuringElement.GetLength(1);
            int seOffsetX = seWidth / 2;
            int seOffsetY = seHeight / 2;

            byte[,] dilatedImage = new byte[imageWidth, imageHeight];

            for (int x = 0; x < imageWidth; x++)
            {
                for (int y = 0; y < imageHeight; y++)
                {
                    byte maxValue = 0; // Initialize with the minimum possible value
                    for (int i = -seOffsetX; i <= seOffsetX; i++)
                    {
                        for (int j = -seOffsetY; j <= seOffsetY; j++)
                        {
                            int pixelX = x + i;
                            int pixelY = y + j;

                            if (pixelX >= 0 && pixelX < imageWidth && pixelY >= 0 && pixelY < imageHeight)
                            {
                                byte pixelValue = inputImage[pixelX, pixelY];
                                int seValue = structuringElement[i + seOffsetX, j + seOffsetY];
                                byte dilatedValue = (byte)(pixelValue + seValue);
                                if (dilatedValue > maxValue)
                                {
                                    maxValue = dilatedValue;
                                }
                            }
                        }
                    }
                    dilatedImage[x, y] = maxValue;
                }
            }

            return dilatedImage;
        }

        // Binary Erosion
        private bool[,] ErodeBinaryImage(bool[,] inputImage, int[,] structuringElement)
        {
            int imageWidth = inputImage.GetLength(0);
            int imageHeight = inputImage.GetLength(1);
            int seWidth = structuringElement.GetLength(0);
            int seHeight = structuringElement.GetLength(1);
            int seOffsetX = seWidth / 2;
            int seOffsetY = seHeight / 2;

            bool[,] erodedImage = new bool[imageWidth, imageHeight];

            for (int x = 0; x < imageWidth; x++)
            {
                for (int y = 0; y < imageHeight; y++)
                {
                    bool minValue = true; // Initialize as true
                    for (int i = -seOffsetX; i <= seOffsetX; i++)
                    {
                        for (int j = -seOffsetY; j <= seOffsetY; j++)
                        {
                            int pixelX = x + i;
                            int pixelY = y + j;

                            if (pixelX >= 0 && pixelX < imageWidth && pixelY >= 0 && pixelY < imageHeight)
                            {
                                bool pixelValue = inputImage[pixelX, pixelY];
                                int seValue = structuringElement[i + seOffsetX, j + seOffsetY];
                                bool erodedValue = pixelValue && (seValue == 1);
                                if (!erodedValue)
                                {
                                    minValue = false;
                                    break; // No need to check further
                                }
                            }
                            else
                            {
                                // If any part of the structuring element goes out of bounds, consider it as false
                                minValue = false;
                                break; // No need to check further
                            }
                        }
                        if (!minValue) break; // No need to check further
                    }
                    erodedImage[x, y] = minValue;
                }
            }

            return erodedImage;
        }

        // Binary Dilation
        private bool[,] DilateBinaryImage(bool[,] inputImage, int[,] structuringElement)
        {
            int imageWidth = inputImage.GetLength(0);
            int imageHeight = inputImage.GetLength(1);
            int seWidth = structuringElement.GetLength(0);
            int seHeight = structuringElement.GetLength(1);
            int seOffsetX = seWidth / 2;
            int seOffsetY = seHeight / 2;

            bool[,] dilatedImage = new bool[imageWidth, imageHeight];

            for (int x = 0; x < imageWidth; x++)
            {
                for (int y = 0; y < imageHeight; y++)
                {
                    bool maxValue = false; // Initialize as false
                    for (int i = -seOffsetX; i <= seOffsetX; i++)
                    {
                        for (int j = -seOffsetY; j <= seOffsetY; j++)
                        {
                            int pixelX = x + i;
                            int pixelY = y + j;

                            if (pixelX >= 0 && pixelX < imageWidth && pixelY >= 0 && pixelY < imageHeight)
                            {
                                bool pixelValue = inputImage[pixelX, pixelY];
                                int seValue = structuringElement[i + seOffsetX, j + seOffsetY];
                                bool dilatedValue = pixelValue || (seValue == 1);
                                if (dilatedValue)
                                {
                                    maxValue = true;
                                    break; // No need to check further
                                }
                            }
                        }
                        if (maxValue) break; // No need to check further
                    }
                    dilatedImage[x, y] = maxValue;
                }
            }

            return dilatedImage;
        }

        // Grayscale Opening
        private byte[,] OpenGrayscaleImage(byte[,] inputImage, int[,] structuringElement)
        {
            // Opening is erosion followed by dilation
            byte[,] erodedImage = ErodeGrayscaleImage(inputImage, structuringElement);
            byte[,] openedImage = DilateGrayscaleImage(erodedImage, structuringElement);
            return openedImage;
        }

        // Grayscale Closing
        private byte[,] CloseGrayscaleImage(byte[,] inputImage, int[,] structuringElement)
        {
            // Closing is dilation followed by erosion
            byte[,] dilatedImage = DilateGrayscaleImage(inputImage, structuringElement);
            byte[,] closedImage = ErodeGrayscaleImage(dilatedImage, structuringElement);
            return closedImage;
        }

        // Binary Opening
        private bool[,] OpenBinaryImage(bool[,] inputImage, int[,] structuringElement)
        {
            // Opening is erosion followed by dilation
            bool[,] erodedImage = ErodeBinaryImage(inputImage, structuringElement);
            bool[,] openedImage = DilateBinaryImage(erodedImage, structuringElement);
            return openedImage;
        }

        // Binary Closing
        private bool[,] CloseBinaryImage(bool[,] inputImage, int[,] structuringElement)
        {
            // Closing is dilation followed by erosion
            bool[,] dilatedImage = DilateBinaryImage(inputImage, structuringElement);
            bool[,] closedImage = ErodeBinaryImage(dilatedImage, structuringElement);
            return closedImage;
        }

        private bool IsBinaryImage(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;

            // Threshold value for distinguishing between binary and grayscale
            int threshold = 128;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114); // Convert to grayscale

                    // If any pixel value is above the threshold, consider it grayscale
                    if (grayValue > threshold)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool[,] AndImages(bool[,] image1, bool[,] image2)
        {
            int width = image1.GetLength(0);
            int height = image1.GetLength(1);

            if (image2.GetLength(0) != width || image2.GetLength(1) != height)
            {
                throw new ArgumentException("Both images must have the same dimensions.");
            }

            bool[,] result = new bool[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = image1[x, y] && image2[x, y];
                }
            }

            return result;
        }

        private bool[,] OrImages(bool[,] image1, bool[,] image2)
        {
            int width = image1.GetLength(0);
            int height = image1.GetLength(1);

            if (image2.GetLength(0) != width || image2.GetLength(1) != height)
            {
                throw new ArgumentException("Both images must have the same dimensions.");
            }

            bool[,] result = new bool[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = image1[x, y] || image2[x, y];
                }
            }

            return result;
        }


        private void AND_BinaryImage_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Check if both pictureBox1 and pictureBox2 have images
                if (pictureBox1.Image == null || pictureBox2.Image == null)
                {
                    MessageBox.Show("Both images must be loaded before performing the AND operation.");
                    return;
                }

                // Convert the images in pictureBox1 and pictureBox2 to binary arrays
                bool[,] image1 = BitmapToBinaryArray(new Bitmap(pictureBox1.Image));
                bool[,] image2 = BitmapToBinaryArray(new Bitmap(pictureBox2.Image));

                // Perform the AND operation on the binary arrays
                bool[,] result = AndImages(image1, image2);

                // Display the result in pictureBox4
                pictureBox4.Image = BinaryArrayToBitmap(result);
                pictureBox4.Refresh();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OR_BinaryImage_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Check if both pictureBox1 and pictureBox2 have images
                if (pictureBox1.Image == null || pictureBox2.Image == null)
                {
                    MessageBox.Show("Both images must be loaded before performing the OR operation.");
                    return;
                }

                // Convert the images in pictureBox1 and pictureBox2 to binary arrays
                bool[,] image1 = BitmapToBinaryArray(new Bitmap(pictureBox1.Image));
                bool[,] image2 = BitmapToBinaryArray(new Bitmap(pictureBox2.Image));

                // Perform the OR operation on the binary arrays
                bool[,] result = OrImages(image1, image2);

                // Display the result in pictureBox4
                pictureBox4.Image = BinaryArrayToBitmap(result);
                pictureBox4.Refresh();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /* private void DisplayBinaryArray(bool[,] binaryArray, PictureBox pictureBox)
         {
             int width = binaryArray.GetLength(0);
             int height = binaryArray.GetLength(1);
             Bitmap resultBitmap = new Bitmap(width, height);

             for (int x = 0; x < width; x++)
             {
                 for (int y = 0; y < height; y++)
                 {
                     Color pixelColor = binaryArray[x, y] ? Color.Black : Color.White;
                     resultBitmap.SetPixel(x, y, pixelColor);
                 }
             }

             pictureBox.Image = resultBitmap;
             pictureBox.Refresh();
         }*/

        private void CountValues_Click(object sender, EventArgs e)
        {
            // Assuming the grayscale image is in pictureBox2
            Bitmap grayscaleImage = new Bitmap(pictureBox2.Image);

            // Compute the histogram of the grayscale image
            int[] histogram = ComputeHistogram(grayscaleImage);

            // Count the number of distinct values
            int distinctValuesCount = histogram.Count(value => value > 0);

            // Display the number of distinct values
            MessageBox.Show($"Number of distinct grayscale values: {distinctValuesCount}");

            // If you want to further display or use the histogram, you can do so here
            // For example, if you want to display the occurrences of each distinct value:
            StringBuilder histogramValues = new StringBuilder();
            for (int i = 0; i < histogram.Length; i++)
            {
                if (histogram[i] > 0)
                {
                    histogramValues.AppendLine($"Value: {i}, Occurrences: {histogram[i]}");
                }
            }
            MessageBox.Show(histogramValues.ToString());
        }
        private void traceBoundary_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please load a binary image first.");
                return;
            }

            bool[,] binaryImage = BitmapToBinaryArray(new Bitmap(pictureBox1.Image));
            List<(int, int)> boundary = TraceBoundary(binaryImage);

            // For visualization: Draw the boundary on the image
            Bitmap boundaryImage = new Bitmap(pictureBox1.Image);
            foreach (var point in boundary)
            {
                boundaryImage.SetPixel(point.Item1, point.Item2, Color.Red); // marking boundary with red color
            }
            pictureBox1.Image = boundaryImage;
            pictureBox1.Refresh();

            MessageBox.Show($"Boundary traced with {boundary.Count} points!");
        }

        private List<(int, int)> TraceBoundary(bool[,] binaryImage)
        {
            int width = binaryImage.GetLength(0);
            int height = binaryImage.GetLength(1);
            List<(int, int)> boundary = new List<(int, int)>();

            // Find the starting point
            (int, int)? start = null;
            for (int y = 0; y < height && start == null; y++)
            {
                for (int x = 0; x < width && start == null; x++)
                {
                    if (binaryImage[x, y])
                    {
                        start = (x, y);
                    }
                }
            }

            if (start == null)
            {
                MessageBox.Show("No foreground pixel found in the image.");
                return boundary;
            }

            int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int dir = 0; // Start direction
            (int, int) pos = start.Value;

            int maxIterations = width * height; // Safety measure
            int iterations = 0;

            do
            {
                boundary.Add(pos);
                for (int i = 0; i < 8; i++)
                {
                    int newX = pos.Item1 + dx[(dir + i) % 8];
                    int newY = pos.Item2 + dy[(dir + i) % 8];
                    if (newX >= 0 && newX < width && newY >= 0 && newY < height && binaryImage[newX, newY])
                    {
                        pos = (newX, newY);
                        dir = (dir + i) % 8;
                        break;
                    }
                }

                iterations++;
                if (iterations > maxIterations)
                {
                    MessageBox.Show("Boundary tracing exceeded the maximum number of iterations. Aborting.");
                    return boundary;
                }
            } while (pos != start.Value);

            return boundary;
        }





        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 3 GO HERE ==============
        // ====================================================================
    }

}