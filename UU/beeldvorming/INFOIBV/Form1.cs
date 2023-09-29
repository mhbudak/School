using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace INFOIBV
{
    public partial class INFOIBV : Form
    {
        private Bitmap InputImage;
        private Bitmap OutputImage;

        public INFOIBV()
        {
            InitializeComponent();
        }

        /*
         * loadButton_Click: process when user clicks "Load" button
         */
        private void loadImageButton_Click(object sender, EventArgs e)
        {
           if (openImageDialog.ShowDialog() == DialogResult.OK)             // open file dialog
            {
                string file = openImageDialog.FileName;                     // get the file name
                imageFileName.Text = file;                                  // show file name
                if (InputImage != null) InputImage.Dispose();               // reset image
                InputImage = new Bitmap(file);                              // create new Bitmap from file
                if (InputImage.Size.Height <= 0 || InputImage.Size.Width <= 0 ||
                    InputImage.Size.Height > 512 || InputImage.Size.Width > 512) // dimension check (may be removed or altered)
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                    pictureBox1.Image = (Image) InputImage;                 // display input image
            }
        }


        /*
         * applyButton_Click: process when user clicks "Apply" button
         */
        private void applyButton_Click(object sender, EventArgs e)
        {
            if (InputImage == null) return;                                 // get out if no input image
            if (OutputImage != null) OutputImage.Dispose();                 // reset output image
            OutputImage = new Bitmap(InputImage.Size.Width, InputImage.Size.Height); // create new output image
            Color[,] Image = new Color[InputImage.Size.Width, InputImage.Size.Height]; // create array to speed-up operations (Bitmap functions are very slow)

            // copy input Bitmap to array            
            for (int x = 0; x < InputImage.Size.Width; x++)                 // loop over columns
                for (int y = 0; y < InputImage.Size.Height; y++)            // loop over rows
                    Image[x, y] = InputImage.GetPixel(x, y);                // set pixel color in array at (x,y)

            // ====================================================================
            // =================== YOUR FUNCTION CALLS GO HERE ====================
            // Alternatively you can create buttons to invoke certain functionality
            // ====================================================================

            byte[,] workingImage = convertToGrayscale(Image);          // convert image to grayscale

            // ==================== END OF YOUR FUNCTION CALLS ====================
            // ====================================================================

            // copy array to output Bitmap
            for (int x = 0; x < workingImage.GetLength(0); x++)             // loop over columns
                for (int y = 0; y < workingImage.GetLength(1); y++)         // loop over rows
                {
                    Color newColor = Color.FromArgb(workingImage[x, y], workingImage[x, y], workingImage[x, y]);
                    OutputImage.SetPixel(x, y, newColor);                  // set the pixel color at coordinate (x,y)
                }
            
            pictureBox2.Image = (Image)OutputImage;                         // display output image
        }


        /*
         * saveButton_Click: process when user clicks "Save" button
         */
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (OutputImage == null) return;                                // get out if no output image
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                OutputImage.Save(saveImageDialog.FileName);                 // save the output image
        }


        /*
         * convertToGrayScale: convert a three-channel color image to a single channel grayscale image
         * input:   inputImage          three-channel (Color) image
         * output:                      single-channel (byte) image
         */
        private byte[,] convertToGrayscale(Color[,] inputImage)
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
                    byte average = (byte)((pixelColor.R + pixelColor.B + pixelColor.G) / 3); // calculate average over the three channels
                    tempImage[x, y] = average;                              // set the new pixel color at coordinate (x,y)
                    progressBar.PerformStep();                              // increment progress bar
                }

            progressBar.Visible = false;                                    // hide progress bar

            return tempImage;
        }


        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 1 GO HERE ==============
        // ====================================================================

        /*
         * invertImage: invert a single channel (grayscale) image
         * input:   inputImage          single-channel (byte) image
         * output:                      single-channel (byte) image
         */
        private byte[,] invertImage(byte[,] inputImage)
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

        private void button1_Click(object sender, EventArgs e)
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
                        byte grayValue = (byte)(0.3 * pixelColor.R + 0.59 * pixelColor.G + 0.11 * pixelColor.B);
                        grayValues[x, y] = grayValue;
                    }
                }

                // Use the invert function
                byte[,] invertedValues = invertImage(grayValues);

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
         * adjustContrast: create an image with the full range of intensity values used
         * input:   inputImage          single-channel (byte) image
         * output:                      single-channel (byte) image
         */
        private byte[,] adjustContrast(byte[,] inputImage)
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

                    // Use the contrast stretching formula to adjust the pixel's intensity.
                    // newValue = (value - min) * 255 / (max - min)
                    byte adjustedValue = (byte)((pixelValue - minIntensity) * 255 / (maxIntensity - minIntensity));

                    // Store the adjusted intensity value in the temporary image.
                    tempImage[x, y] = adjustedValue;
                }
            }

            // Return the adjusted image.
            return tempImage;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Ensure there's an image loaded in the PictureBox.
            if (pictureBox1.Image != null)
            {
                // Convert the image in the PictureBox to a Bitmap so we can manipulate its pixels.
                Bitmap bitmap = new Bitmap(pictureBox1.Image);

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
                        byte grayValue = (byte)(0.3 * pixelColor.R + 0.59 * pixelColor.G + 0.11 * pixelColor.B);
                        grayValues[x, y] = grayValue;
                    }
                }

                // Use the adjustContrast function to get the contrast-adjusted grayscale values.
                byte[,] adjustedValues = adjustContrast(grayValues);

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
         * createGaussianFilter: create a Gaussian filter of specific square size and with a specified sigma
         * input:   size                length and width of the Gaussian filter (only odd sizes)
         *          sigma               standard deviation of the Gaussian distribution
         * output:                      Gaussian filter
         */
        private double[,] createGaussianFilter(int size, double sigma)
        {
            if (size % 2 == 0)
            {
                throw new ArgumentException("Only odd sizes are accepted.");
            }

            double[,] filter = new double[size, size];
            int halfSize = size / 2;
            double sum = 0.0;

            for (int x = -halfSize; x <= halfSize; x++)
            {
                for (int y = -halfSize; y <= halfSize; y++)
                {
                    double value = (1.0 / (2.0 * Math.PI * sigma * sigma)) *
                                   Math.Exp(-(x * x + y * y) / (2.0 * sigma * sigma));
                    filter[x + halfSize, y + halfSize] = value;
                    sum += value;
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    filter[i, j] /= sum;
                }
            }

            return filter;
        }

        /*
         * convolveImage: apply linear filtering of an input image
         * input:   inputImage          single-channel (byte) image
         *          filter              linear kernel
         * output:                      single-channel (byte) image
         */
        private byte[,] convolveImage(byte[,] srcImage, double[,] kernel)
        {
            int width = srcImage.GetLength(0);
            int height = srcImage.GetLength(1);

            byte[,] result = new byte[width, height];
            int foff = (kernel.GetLength(0) - 1) / 2;

            for (int y = foff; y < height - foff; y++)
            {
                for (int x = foff; x < width - foff; x++)
                {
                    double pixelValue = 0.0;

                    for (int fy = -foff; fy <= foff; fy++)
                    {
                        for (int fx = -foff; fx <= foff; fx++)
                        {
                            pixelValue += (double)(srcImage[x + fx, y + fy]) * kernel[fy + foff, fx + foff];
                        }
                    }

                    if (pixelValue > 255) pixelValue = 255;
                    else if (pixelValue < 0) pixelValue = 0;

                    result[x, y] = (byte)pixelValue;
                }
            }

            // Handle borders as per your requirement
            // For this example, we're just copying the border pixels from the source image.
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                Bitmap grayscaleImageBitmap = new Bitmap(pictureBox2.Image);

                // Convert the grayscale Bitmap to byte[,] format for convolution
                byte[,] grayscaleArray = BitmapToByteArray(grayscaleImageBitmap);

                // Create the Gaussian kernel
                double[,] gaussianKernel = createGaussianFilter(3, 1.0); // Adjust size and sigma as needed

                // Convolve the grayscale image with the Gaussian kernel
                byte[,] convolvedArray = convolveImage(grayscaleArray, gaussianKernel);

                // Convert the convolved byte[,] array back to a Bitmap
                Bitmap convolvedBitmap = ByteToBitmap(convolvedArray);

                // Set the convolved Bitmap to pictureBox2
                pictureBox2.Image = convolvedBitmap;
                pictureBox2.Refresh();
            }
        }

        private byte[,] BitmapToByteArray(Bitmap bmp)
        {
            byte[,] result = new byte[bmp.Width, bmp.Height];
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pixel = bmp.GetPixel(x, y);
                    result[x, y] = (byte)((pixel.R + pixel.G + pixel.B) / 3);  // Should already be grayscale, but averaging channels ensures it.
                }
            }
            return result;
        }

        private Bitmap ByteToBitmap(byte[,] byteArray)
        {
            int width = byteArray.GetLength(0);
            int height = byteArray.GetLength(1);
            Bitmap result = new Bitmap(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
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
        private byte[,] medianFilter(byte[,] inputImage, int size)
        {
            if (size % 2 == 0)
            {
                throw new ArgumentException("Only odd sizes are accepted.");
            }

            int width = inputImage.GetLength(0);
            int height = inputImage.GetLength(1);
            int offset = size / 2;

            byte[,] outputImage = new byte[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    List<byte> neighbors = new List<byte>();

                    for (int i = -offset; i <= offset; i++)
                    {
                        for (int j = -offset; j <= offset; j++)
                        {
                            // Check if the neighbor is outside the image boundaries
                            int neighborX = x + i;
                            int neighborY = y + j;

                            if (neighborX < 0) neighborX = 0;
                            if (neighborY < 0) neighborY = 0;
                            if (neighborX >= width) neighborX = width - 1;
                            if (neighborY >= height) neighborY = height - 1;

                            neighbors.Add(inputImage[neighborX, neighborY]);
                        }
                    }
                    neighbors.Sort();
                    outputImage[x, y] = neighbors[neighbors.Count / 2];  // The median value
                }
            }
            return outputImage;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                Bitmap grayscaleImageBitmap = new Bitmap(pictureBox2.Image);

                // Convert the grayscale Bitmap to byte[,] format
                byte[,] grayscaleArray = BitmapToByteArray(grayscaleImageBitmap);

                // Get the median filter size. For this example, we'll use a size of 3. Adjust as needed.
                int filterSize = 3;

                // Apply the median filter
                byte[,] filteredArray = medianFilter(grayscaleArray, filterSize);

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
        private byte[,] edgeMagnitude(byte[,] inputImage, sbyte[,] horizontalKernel, sbyte[,] verticalKernel)
        {
            int width = inputImage.GetLength(0);
            int height = inputImage.GetLength(1);
            byte[,] result = new byte[width, height];

            byte[,] Gx = convolveImage(inputImage, ConvertToDoubleArray(horizontalKernel));
            byte[,] Gy = convolveImage(inputImage, ConvertToDoubleArray(verticalKernel));

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
        private void button6_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap grayscaleImageBitmap = new Bitmap(pictureBox1.Image);

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
                byte[,] edgeMagnitudeArray = edgeMagnitude(grayscaleArray, horizontalKernel, verticalKernel);

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
        private byte[,] thresholdImage(byte[,] inputImage, byte threshold = 100)
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

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)  // Assuming you want to threshold the image in pictureBox2
            {
                Bitmap imageBitmap = new Bitmap(pictureBox2.Image);

                // Convert the Bitmap to byte[,] format
                byte[,] imageArray = BitmapToByteArray(imageBitmap);

                // Call the thresholdImage function
                byte[,] thresholdedArray = thresholdImage(imageArray);

                // Convert the thresholded byte[,] array back to a Bitmap
                Bitmap thresholdedBitmap = ByteToBitmap(thresholdedArray);

                // Set the thresholded Bitmap to pictureBox2 (or wherever you want to display the thresholded image)
                pictureBox2.Image = thresholdedBitmap;
                pictureBox2.Refresh();
            }
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void INFOIBV_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
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

       


        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 2 GO HERE ==============
        // ====================================================================


        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 3 GO HERE ==============
        // ====================================================================

    }
}