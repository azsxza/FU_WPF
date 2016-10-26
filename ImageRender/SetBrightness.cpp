#include"global.h"

Mat setbri (Mat a, int b)
{
	Mat dd = a.clone ();
	for (int y = 0; y < dd.rows; y++)
	{
		for (int x = 0; x < dd.cols; x++)
		{
			for (int c = 0; c < 3; c++)
			{
				dd.at<Vec3b> (y, x)[c] = saturate_cast<uchar>((80 * 0.01)*(dd.at<Vec3b> (y, x)[c]) + b);
			}
		}
	}
	return dd;
}

void setbri (uchar* src, uchar* dest, int width, int height, int stride, int b)
{
	int channel = stride / width;
	for (int y = 0; y < height; y++)
	{
		for (int x = 0; x < width; x++)
		{
			for (int c = 0; c < 3; c++)
			{
				dest[y*stride + x*channel + c] = (80 * 0.01)*(src[y*stride + x*channel + c]) + b;
			}
			if(channel==4)
				dest[y*stride + x*channel + 3] = (80 * 0.01)*(src[y*stride + x*channel + 3]) + b;
		}
	}
}