#include"global.h"

Mat MtxxCQ (const Mat& img, float opacity)
{

	Mat texture = imread ("mtxx_caiqian.jpg", 1);
	resize (texture, texture, img.size ());

	/// ��ʽת��
	Mat I;
	img.convertTo (I, CV_32FC3, 1 / 255.0);
	texture.convertTo (texture, CV_32FC3, 1 / 255.0);

	/// ��Ļ��ʽ��ϣ�f(a,b)= a + b - a*b
	Mat caiqian;
	Mat temp;
	multiply (I, texture, temp);

	caiqian = I + texture - temp;

	/// ���ӷ�ʽ���
	for (int nY = 0;nY<texture.rows;nY++)
	{
		for (int nX = 0;nX<texture.cols;nX++)
		{
			caiqian.at<Vec3f> (nY, nX)[0] = caiqian.at<Vec3f> (nY, nX)[0] < 0.5 ? 2 * caiqian.at<Vec3f> (nY, nX)[0] * texture.at<Vec3f> (nY, nX)[0] : 1 - 2 * (1 - caiqian.at<Vec3f> (nY, nX)[0])*(1 - texture.at<Vec3f> (nY, nX)[0]);
			caiqian.at<Vec3f> (nY, nX)[1] = caiqian.at<Vec3f> (nY, nX)[1] < 0.5 ? 2 * caiqian.at<Vec3f> (nY, nX)[1] * texture.at<Vec3f> (nY, nX)[1] : 1 - 2 * (1 - caiqian.at<Vec3f> (nY, nX)[1])*(1 - texture.at<Vec3f> (nY, nX)[1]);
			caiqian.at<Vec3f> (nY, nX)[2] = caiqian.at<Vec3f> (nY, nX)[2] < 0.5 ? 2 * caiqian.at<Vec3f> (nY, nX)[2] * texture.at<Vec3f> (nY, nX)[2] : 1 - 2 * (1 - caiqian.at<Vec3f> (nY, nX)[2])*(1 - texture.at<Vec3f> (nY, nX)[2]);
		}
	}

	//��͸���ȼ���
	caiqian = I*(100 - opacity) / 100.0f + caiqian*opacity / 100.0f;

	caiqian.convertTo (caiqian, CV_8UC3, 255.0);

	return caiqian;
}

void MtxxCQ (uchar* Src, uchar* Dest, int Width, int Height, int Stride, float opacity)
{
	int channel = Stride / Width;
	Mat I = cv::Mat (Height, Width, CV_MAKETYPE (CV_8U, channel), Src, Stride);

	if (channel == 4)
		cvtColor (I, I, COLOR_BGRA2BGR);
	Mat dst = MtxxCQ (I, opacity);

	for (int nY = 0;nY <dst.rows;nY++)
	{
		for (int nX = 0;nX<dst.cols;nX++)
		{
			Dest[nY*Stride + channel*nX] = dst.data[nY*dst.step + 3 * nX];
			Dest[nY*Stride + channel*nX + 1] = dst.data[nY*dst.step + 3 * nX + 1];
			Dest[nY*Stride + channel*nX + 2] = dst.data[nY*dst.step + 3 * nX + 2];
		}
	}
	

}