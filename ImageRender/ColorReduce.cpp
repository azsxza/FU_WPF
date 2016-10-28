#include"global.h"

Mat ColorReduce (Mat I, int div)
{
	Mat src = I.clone ();
	uchar*ppp = src.ptr (0);
	uchar* p = I.ptr (0);
	for (int i = 0;i < I.cols*I.rows * I.channels();i++)
		ppp[i] = saturate_cast<uchar>((p[i] - (p[i] % div) + div / 2));
	return src;
}

void ColorReduce (uchar* src, uchar* dest, int height, int width, int stride, int div)
{
	int channel = stride / width;
	Mat I = cv::Mat (height, width, CV_MAKETYPE (CV_8U, channel), src, stride);
	Mat T = ColorReduce (I, div);
	for (int i = 0;i < width*height * channel;i++)
		dest[i] = T.data[i];
}
