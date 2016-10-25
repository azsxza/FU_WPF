#include"global.h"

Mat OilPaint (const Mat& I, int brushSize, int coarseness)
{
	CV_Assert (!I.empty ());

	if (brushSize < 1) brushSize = 1;
	if (brushSize > 8) brushSize = 8;

	if (coarseness < 1) coarseness = 1;
	if (coarseness > 255) coarseness = 255;

	int width = I.cols;
	int height = I.rows;

	int lenArray = coarseness + 1;
	int* CountIntensity = new int[lenArray];
	uint* RedAverage = new uint[lenArray];
	uint* GreenAverage = new uint[lenArray];
	uint* BlueAverage = new uint[lenArray];

	/// ͼ��ҶȻ�
	Mat gray;
	cvtColor (I, gray, COLOR_BGR2GRAY);


	/// Ŀ��ͼ��
	Mat dst = Mat::zeros (I.size (), I.type ());

	for (int nY = 0;nY <height; nY++)
	{
		// �ͻ���Ⱦ��Χ���±߽�
		int top = nY - brushSize;
		int bottom = nY + brushSize + 1;

		if (top<0) top = 0;
		if (bottom >= height) bottom = height - 1;

		for (int nX = 0;nX<width;nX++)
		{
			// �ͻ���Ⱦ��Χ���ұ߽�
			int left = nX - brushSize;
			int right = nX + brushSize + 1;

			if (left<0) left = 0;
			if (right >= width) right = width - 1;

			//��ʼ������
			for (int i = 0;i <lenArray;i++)
			{
				CountIntensity[i] = 0;
				RedAverage[i] = 0;
				GreenAverage[i] = 0;
				BlueAverage[i] = 0;
			}


			// ���������ѭ������������Ĵ�ѭ��
			// Ҳ���ͻ���Ч����Ĺؼ�����
			for (int j = top;j<bottom;j++)
			{
				for (int i = left;i<right;i++)
				{
					uchar intensity = static_cast<uchar>(coarseness*gray.at<uchar> (j, i) / 255.0);
					CountIntensity[intensity]++;

					RedAverage[intensity] += I.at<Vec3b> (j, i)[2];
					GreenAverage[intensity] += I.at<Vec3b> (j, i)[1];
					BlueAverage[intensity] += I.at<Vec3b> (j, i)[0];
				}
			}

			// �����ֵ������¼����������
			uchar chosenIntensity = 0;
			int maxInstance = CountIntensity[0];
			for (int i = 1;i<lenArray;i++)
			{
				if (CountIntensity[i]>maxInstance)
				{
					chosenIntensity = (uchar)i;
					maxInstance = CountIntensity[i];
				}
			}

			dst.at<Vec3b> (nY, nX)[2] = saturate_cast<uchar>(RedAverage[chosenIntensity] / static_cast<float>(maxInstance));
			dst.at<Vec3b> (nY, nX)[1] = saturate_cast<uchar>(GreenAverage[chosenIntensity] / static_cast<float>(maxInstance));
			dst.at<Vec3b> (nY, nX)[0] = saturate_cast<uchar>(BlueAverage[chosenIntensity] / static_cast<float>(maxInstance));
		}

	}

	delete[] CountIntensity;
	delete[] RedAverage;
	delete[] GreenAverage;
	delete[] BlueAverage;

	return dst;
}


void OilPaint (uchar* Src, uchar* Dest, int Width, int Height, int Stride, int brushSize, int coarseness)
{
	int channel = Stride / Width;
	Mat I = cv::Mat (Height, Width, CV_MAKETYPE (CV_8U, channel), Src, Stride);

	if (channel == 4)
		cvtColor (I, I, COLOR_BGRA2BGR);

	Mat OilI = OilPaint (I, brushSize, coarseness);

	if (channel == 4)
		cvtColor (OilI, OilI, COLOR_BGR2BGRA);
	for (int i = 0;i < OilI.cols*OilI.rows * channel;i++)
		Dest[i] = OilI.data[i];
}