#include <stdio.h>
//#include "stdlib.h"
#include "sys.h"
#include "jinclude.h"
#include "jcapi.h"
#include "w25qxx.h"
#include "test.h"

int BMP_JPG(void);
extern u8 buffer[960];
extern u8 buff[];
extern unsigned char JPG_enc_buf[6000];  //jpeg �������
extern unsigned int pt_buf;     //������ָ��
jpeg_compress_info *cinfo;
jpeg_compress_info info1;

JQUANT_TBL  JQUANT_TBL_2[2];

JHUFF_TBL  JHUFF_TBL_4[4];

unsigned char dcttab[3][512];//

//volatile unsigned char inbuf_buf[1920];
extern u8 inbuf_buf[];
////����������,�����Ϊ���240��ͼƬ��С���õģ����Ҫ�����ͼƬ������Ҫ����Ļ���11520 = 16x40x3

int BMP_JPG(void) 
{
  int width, height;

//  int t;
  
  JSAMPLE *image;//ͼ��Դ����ָ��

  //unsigned char string[20];

  //jpeg_compress_info *cinfo;
  
  pt_buf = 0;

  //image =(JSAMPLE *)p;//(JSAMPLE *)jutl_read_bitmap(src, &width, &height);
  width = 320;//ͼ��Ŀ�� 
  height = 1;//ͼ��ĸ߶� 
  cinfo = jpeg_create_compress();
  if (!cinfo) 
  {
    //printf("error in create cinfo, malloc faild!\n");
  }
  cinfo->image_width = width;
  cinfo->image_height= height;
  cinfo->output =(char *)JPG_enc_buf;//fopen("test.jpg", "wb");
  jpeg_set_default(cinfo);  
  
  jpeg_start_compress(cinfo);
  
  while (cinfo->next_line < cinfo->image_height) 
  {
    /*printf("%d\n", i/240/3);*/
	  W25QXX_Read(buff,8192,960);
    jpeg_write_scanline(cinfo, buff);
    //��Ϊ������ram��С����������%1920����ѭ���ظ��������ĳ����Լ�������
   
  }
  jpeg_finish_compress(cinfo);
  //fclose(cinfo->output);
  
  jpeg_destory_compress(cinfo);
	return 0;
}


