#include "led.h"
#include "delay.h"
#include "sys.h"
#include "usart.h"	 
#include "string.h"
#include "stdlib.h"
#include <stdio.h>
#include "jinclude.h"
#include "jcapi.h"
#include "lcd.h"
#include "key.h"
#include "malloc.h"
#include "usart3.h"
#include "usart.h"
#include "wifista.h"
#include "spi.h"

unsigned char JPG_enc_buf[5000];  //jpeg ������塡����ͼƬ��С����
unsigned int pt_buf =0;//������ָ��
static volatile ErrorStatus HSEStartUpStatus = SUCCESS;
//static const u8 bmpheader[54]={//����bmp�ײ�����תBMP��ʽ��
//	0x42, 0x4D, 0x36, 0x84, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x36, 0x00, 0x00, 0x00, 0x28, 0x00, 
//	0x00, 0x00, 0x40, 0x01, 0x00, 0x00, 0xF0, 0x00, 0x00, 0x00, 0x01, 0x00, 0x18, 0x00, 0x00, 0x00, 
//	0x00, 0x00, 0x00, 0x84, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
//	0x00, 0x00, 0x00, 0x00, 0x00, 0x00
//};
jpeg_compress_info info1;
JQUANT_TBL  JQUANT_TBL_2[2];
JHUFF_TBL  JHUFF_TBL_4[4];
unsigned char dcttab[3][512];
u8 buffer[128];
u32 FLASH_SIZE1 = 8192;
u8 R,G,B;
u8 inbuf_buf[2048];//���뻺�����ͼƬ��С����

u8 jpeg_data[320];

extern u8 USART3_RX_BUF[USART3_MAX_RECV_LEN]; 
extern u16 USART3_RX_STA;
u8 SPISendData = 0xA5;

//��������
void jpegSemd2PC(void);


void jpegCompress() //jpegѹ��
{
	//jpeg var
	u8 buf[4];
	jpeg_compress_info *cinfo;
		pt_buf = 0;

	int width, height;
	int j,i;

		//jpeg compress
		width = 256;//
		height = 128;//
		cinfo = jpeg_create_compress();  //����jpeg����
		if (!cinfo) 
		{
			printf("error in create cinfo, malloc faild!\n");
		}
		cinfo->image_width = width;
		cinfo->image_height= height;
		cinfo->output =(char *)JPG_enc_buf;//fopen("test.jpg", "wb");
		jpeg_set_default(cinfo);  
		  
		  jpeg_start_compress(cinfo);  //��ʼiѹ��
		
		
//		for(i = 2 ; i>=0; i--)
//			jpeg_data[i] = SPI1_ReadWriteByte(SPISendData) * 2;  //��ˮ���ȹ�������ʱ������
		
		for(j=0;j<height;j++)
		{	//printf("��%u��",j);
			for(i=width -1;i>=0;i--)
			{
				jpeg_data[i] = SPI1_ReadWriteByte(SPISendData) * 2;
			}
			jpeg_write_scanline(cinfo, jpeg_data);
		}
		
		jpeg_finish_compress(cinfo);  
		jpeg_destory_compress(cinfo);
		
}

void jpegSend2PC(void)
{
	
		u8 *p;
//		u8 buf[4];
		int t = 0;

//		buf[0] = pt_buf >> 24;     
//		buf[1] = pt_buf >> 16;
//		buf[2] = pt_buf >> 8;
//		buf[3] = pt_buf;
	
		p=mymalloc(SRAMIN,32);							//����32�ֽ��ڴ�
		sprintf((char*)p,"PicResponse;%d;",pt_buf);//�������߲���:ssid,����
	
		u3_printf(p);

//		for(t = 0;t<4;t++)
//		{
//			UART3_Send_Data(buf[t]);		
//		}
		for(t = 0;t<pt_buf;t++)
		{
			UART3_Send_Data(JPG_enc_buf[t]);		
		}
		USART3_RX_STA = 0;
		if(USART3_RX_BUF[0] == 0xA3)
		{
			SPISendData = USART3_RX_BUF[1];
			USART3_RX_STA = 0;
		}
		myfree(SRAMIN,p);
}

void jpegSend2OneNet(void)
{
	u8 *p;
	int t =0;
	p=mymalloc(SRAMIN,32);							//����32�ֽ��ڴ�
	sprintf((char*)p,"Content-Length:%d\r\n\r\n",pt_buf);//�������߲���:ssid,����
	u3_printf("POST http://api.heclouds.com/bindata?device_id=607169499&datastream_id=image&desc=testfile HTTP/1.1\r\n");
	u3_printf("api-key:your master api key\r\n"); //����master api key���޸���
	u3_printf("Host:api.heclouds.com\r\n");
	u3_printf(p);
	for(t = 0;t<pt_buf;t++)
	{
		UART3_Send_Data(JPG_enc_buf[t]);		
	}
	myfree(SRAMIN,p);
}

u8 GetOneNetDataPoiont(void)
{
	USART3_RX_STA=0;
	u3_printf("GET http://api.heclouds.com/devices/607169499/datastreams/direction HTTP/1.1\r\n");
	u3_printf("api-key:your master api key\r\n"); //����master api key���޸���
	u3_printf("Host:api.heclouds.com\r\n\r\n");  
	delay_ms(100);
		USART3_RX_STA=0;
	char *c1=strstr(USART3_RX_BUF, "current_value");

	return *(c1+16);
}

void OnNetCarControl(void)
{
	char direction = GetOneNetDataPoiont();
	if(direction == '1') //��ǰ
	{
		SPISendData=0x1A;
	}
	else if(direction == '2')//����
	{
		SPISendData=0x3A;
	}
	else if(direction == '3')//����
	{
		SPISendData=0x4A;
	}
	else if(direction == '4') // ֹͣ
	{
		SPISendData=0x2A;
	}
}

int main(void)
{ 
	u8 key = 0;
	int  k = 0;
	u8 flag = 0;
	
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_2);//����ϵͳ�ж����ȼ�����2
	delay_init(168);     //��ʼ����ʱ����
	uart_init(115200);	//��ʼ�����ڲ�����Ϊ115200
	usart3_init(115200);		//��ʼ������3 	 
	KEY_Init();
	SPI1_Init();
	LED_Init();					//��ʼ��LED 

	
	while(1)
	{
		key=KEY_Scan(0);//��֧������
		if(key==KEY1_PRES)
		{
			atk_8266_quit_trans();
			atk_8266_wifista_connectPC_config();
//			printf("quit trans");
			flag = 1;
		}
		else if(key == KEY0_PRES)
		{
			flag = 2;
			atk_8266_quit_trans();
			atk_8266_wifista_connectOneNET_config();
		}
		else if(flag == 1)
		{
			jpegCompress();  //jpegѹ��
			jpegSend2PC();  //�����PC
		}
		else if(flag == 2)
		{
			jpegCompress();  //jpegѹ��
			OnNetCarControl();  //��ȡС������ָ��
			jpegSend2OneNet(); //����ͼƬ��OneNET

		}

		delay_ms(100);

		k++;
		if(k==20)
		{
			LED0=!LED0;//��ʾϵͳ��������	
			k=0;
		}		   
	}       
}

