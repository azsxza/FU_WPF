#include<iostream>
#include<algorithm>
#include<vector>
#include<string>
#include<math.h>
#include<Windows.h>
#include<thread>
#include<opencv2\opencv.hpp>

using namespace std;
using namespace cv;

enum TRANSFERMODE
{
	TRANSFERMODE_SHADOWS,		//��Ӱ
	TRANSFERMODE_MIDTONES,	//�м��
	TRANSFERMODE_HIGHLIGHTS,	//�߹�
};

enum LineStyle
{
	DOG, FDOG
};

#ifndef MAX
#define MAX(a, b) ((a) > (b) ? (a) : (b))
#endif
#ifndef MIN
#define MIN(a, b) ((a) < (b) ? (a) : (b))
#endif

#define ROUND(x) ((int) ((x) + 0.5))

#ifndef CLAMP
#define CLAMP(x,l,u) ((x)<(l)?(l):((x)>(u)?(u):(x)))
#endif

#define CLAMP0255(x) ((x)<(0)?(0):((x)>(255)?(255):(x)))
#define SQR(x) ((x)*(x))


#define CLAMP0255(x) ((x)<(0)?(0):((x)>(255)?(255):(x)))


Mat CoherenceFilter (const Mat& img, int sigma, int str_sigma, float blend, int iter);
Mat BalanceColor (Mat bitmap, int cyan, int magenta, int yellow, TRANSFERMODE mode, bool preserveLuminosity);
Mat AdjustCurve (Mat Src, uchar (&r_lookup)[256], uchar (&g_lookup)[256], uchar (&b_lookup)[256]);
Mat PreserveLuminosityAdjustCurve (Mat I, uchar (&r_lookup)[256], uchar (&g_lookup)[256], uchar (&b_lookup)[256]);
static int hslValueInt (double n1, double n2, double hue);
void Rgb2Hsl_Int (int& red, int& green, int& blue);
int Rgb2Hsl_L (uchar red, uchar green, uchar blue);
void Hsl2Rgb_Int (int& hue, int& saturation, int& lightness);
int Clamp (int v, int min, int max);
Mat GenETFFromGradientMap (cv::Mat src, int kernel = 5, int iter = 3);
Mat GenETFFromInputImage (cv::Mat src, int kernel = 5, int iter = 3);
Mat GetEdgeTangentFlow (cv::Mat src);
Mat VisualizeByArrow (cv::Mat src);
void MakeUnit (cv::Vec2f& v);
void VisualizeByLIC (cv::Mat src);
Mat BlendImage (Mat I1, Mat I2);
Mat RotateImage (const Mat source, double angle, int border = 20);

//��Ƶʵʱ��������ϵĺ���
Mat PencilDraw (Mat I, int ks, int dirNum, double gamma, Mat P);
Mat ColorPencilDraw (Mat I2, int ks, int dirNum, double gamma, Mat P2);
Mat MtxxCQ (const Mat& img, float opacity);
Mat MtxxLifeSketch (Mat I, float opacity);
Mat Worlde (Mat src);
Mat Carving (Mat src2);
Mat Cartoon2 (Mat src2);
Mat Quban (Mat I);
Mat Maoboli (Mat I);
Mat Mosaic (Mat I, int size);




//Ǧ�ʻ� 1
extern "C" __declspec(dllexport) void PencilDraw (uchar* I, uchar* output, int rows, int cols, int stride, int ks, int dirNum, double gamma);

//��ɫǦ�ʻ� 2
extern "C" __declspec(dllexport) void ColorPencilDraw (uchar* I, uchar* output, int rows, int cols, int stride, int ks, int dirNum, double gamma);

//GTA5��� 3 
extern "C" __declspec(dllexport) void GTA5Style (uchar* Src, uchar* Dest, int Width, int Height, int Stride, int brushSize, int coarseness);

//��ͨ 4
extern "C" __declspec(dllexport) void  Cartoon (uchar* Src, uchar* Dest, int Width, int Height, int Stride, float sigma_s, float sigma_r);

//���� 5
extern "C" __declspec(dllexport) void CoherenceFilter (uchar* Src, uchar* Dest, int Width, int Height, int Stride, int sigma, int str_sigma, float blend, int iter);

//������ 6
extern "C" __declspec(dllexport) void  LineDrawing (uchar* Src, uchar* Dest, int Width, int Height, int Stride, int kernel, int iter, float sigma_c, float sigma_m, LineStyle style);

//ĥƤ 7
extern "C" __declspec(dllexport) void Dermabrasion (uchar* Src, uchar* Dest, int Width, int Height, int Stride, int ks, int opacity, int white);

//�ͻ� 8
extern "C" __declspec(dllexport) void OilPaint (uchar* Src, uchar* Dest, int Width, int Height, int Stride, int brushSize, int coarseness);

//��ͼ�����ͻ� 9
extern "C" __declspec(dllexport) void MtxxOilPaint (uchar* Src, uchar* Dest, int Width, int Height, int Stride, float opacity);

//��ͼ�����ɫǦ�ʻ� 10
extern "C" __declspec(dllexport) void MtxxCQ (uchar* Src, uchar* Dest, int Width, int Height, int Stride, float opacity);

//��ͼ����д��Ǧ�ʻ� 11
extern "C" __declspec(dllexport) void MtxxLifeSketch (uchar* Src, uchar* Dest, int Width, int Height, int Stride, float opacity);

//ˮ�ʻ� 12 
extern "C" __declspec(dllexport) void WaterColor (uchar* Src, uchar* Dest, int Width, int Height,int stride, int brushSize_w, int brushSize_h);

//����ɫ�� 13 !!!
extern "C" __declspec(dllexport) void SetHue (uchar* Src, uchar* Dest, int height, int width, int n, int stride);

//���ñ��Ͷ� 14  !!!
extern "C" __declspec(dllexport) void SetSaturation (uchar* Src, uchar* Dest, int height, int width, int n, int stride);

// ���� 15
extern "C" __declspec(dllexport) void Worlde (uchar* src, uchar* dest, int height, int width, int stride);

//��� 16
extern "C" __declspec(dllexport) void Carving (uchar* src, uchar*dest, int height, int width, int stride);

//��ͨ2 17   ��ʱ������
extern "C" __declspec(dllexport) void Cartoon2 (uchar* src, uchar* dest, int height, int width, int stride);

//��� 18 !!!
extern "C" __declspec(dllexport) void Quban (uchar* src, uchar* dest, int stride, int height, int width);

//ë���� 19
extern "C" __declspec(dllexport) void Maoboli (uchar* src, uchar* dest, int stride, int height, int width);

//������ 20
extern "C" __declspec(dllexport) void Mosaic (uchar* src, uchar* dest, int stride, int height, int width,int size);

//ascii������ 21
extern "C" __declspec(dllexport) void ascii (uchar* src, uchar*dest, int width, int height, int stride);

//edgePreservingFilter  22
extern "C" __declspec(dllexport) void edgePreservingFilter (uchar*src, uchar*dest, int height, int width, int stride);

//detailEnhance 23
extern "C" __declspec(dllexport) void detailEnhance (uchar* src, uchar* dest, int height, int width, int stride);

//�۱ʻ� 25
extern "C" __declspec(dllexport) void Fenbi (uchar* src, uchar* dest, int height, int width, int stride);

//LowPoly��� 26
extern "C" __declspec(dllexport) void Lowpoly (uchar* src, uchar*dest, int index, int width, int height, int stride);

//����2 27
extern "C" __declspec(dllexport) void Line (uchar* src, uchar*dest, int width, int height, int stride);

//������ɫ 28
extern "C" _declspec(dllexport) void ColorReduce (uchar* src, uchar* dest, int height, int width, int stride, int div);

//................ 29
extern "C" _declspec(dllexport) void shan (uchar* src, uchar* dest, double ddd, int height, int width, int stride);



//��������
extern "C" __declspec(dllexport) void setbri (uchar* src, uchar* dest, int height, int width, int stride, int b);

//�����Աȶ�
extern "C" __declspec(dllexport) void setCompare (uchar* src, uchar* dest, int height, int width, int stride, int b);





//face
extern "C" __declspec(dllexport) void inti ();
extern "C" __declspec(dllexport) void GetKeyPoint (uchar* imgData, int* key, int height, int width);