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
extern unsigned char JPG_enc_buf[6000];  //jpeg 输出缓冲
extern unsigned int pt_buf;     //缓冲区指针
jpeg_compress_info *cinfo;
jpeg_compress_info info1;

JQUANT_TBL  JQUANT_TBL_2[2];

JHUFF_TBL  JHUFF_TBL_4[4];

unsigned char dcttab[3][512];//

//volatile unsigned char inbuf_buf[1920];
extern u8 inbuf_buf[];
////输入区缓冲,这个是为宽度240的图片大小设置的，如果要更大的图片，就需要更大的缓冲11520 = 16x40x3

int BMP_JPG(void) 
{
  int width, height;

//  int t;
  
  JSAMPLE *image;//图像源数据指针

  //unsigned char string[20];

  //jpeg_compress_info *cinfo;
  
  pt_buf = 0;

  //image =(JSAMPLE *)p;//(JSAMPLE *)jutl_read_bitmap(src, &width, &height);
  width = 320;//图像的宽度 
  height = 1;//图像的高度 
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
    //因为输入区ram很小，所以用了%1920做了循环重复，正常的程序自己做处理
   
  }
  jpeg_finish_compress(cinfo);
  //fclose(cinfo->output);
  
  jpeg_destory_compress(cinfo);
	return 0;
}


