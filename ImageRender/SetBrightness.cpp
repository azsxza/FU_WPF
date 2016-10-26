#include"global.h"

void setbri (uchar* src, uchar* dest, int height, int width, int stride, int b)
{
	int channel = stride / width;
	for (int y = 0; y < height; y++)
	{
		for (int x = 0; x < width; x++)
		{
			for (int c = 0; c < 3; c++)
			{
				dest[y * channel * width + x * channel + c] = saturate_cast<uchar>((80 * 0.01)*(src[y * channel * width + x * channel + c]) + b);
			}
			if(channel==4)
				dest[y * channel * width + x * channel + 3] = saturate_cast<uchar>((80 * 0.01)*(src[y * channel * width + x * channel + 3]) + b);
		}
	}
}