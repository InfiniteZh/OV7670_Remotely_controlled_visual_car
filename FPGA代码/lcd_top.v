/*-------------------------------------------------------------------------
This confidential and proprietary software may be only used as authorized
by a licensing agreement from amfpga.
(C) COPYRIGHT 2013.www.amfpga.com ALL RIGHTS RESERVED
Filename			:		sdram_ov7670_vga.v
Author				:		Amfpga
Data				:		2013-02-1
Version				:		1.0
Description			:		sdram vga controller with ov7670 display.
Modification History	:
Data			By			Version			Change Description
===========================================================================
13/02/1
--------------------------------------------------------------------------*/
module lcd_top
(  	
	//global clock
	input			clk,			//system clock
	input			rst_n,     		//sync reset
	
	//lcd interface
	output			lcd_dclk,   	//lcd pixel clock
	output			lcd_blank,		//lcd blank
	output			lcd_sync,		//lcd sync
	output			lcd_hs,	    	//lcd horizontal sync
	output			lcd_vs,	    	//lcd vertical sync
	output			lcd_en,			//lcd display enable
	output	[7:0]	lcd_rgb,		//lcd display data

	//user interface
	output			lcd_request,	//lcd data request
	output			lcd_framesync,	//lcd frame sync
	output	[10:0]	lcd_xpos,		//lcd horizontal coordinate
	output	[10:0]	lcd_ypos,		//lcd vertical coordinate
	input	[15:0]	lcd_data	//lcd data
	
);	  


wire [7:0]	Y;		//lcd red data
wire hs;
wire vs;
wire en;
assign en=1'b1;
wire [15:0]lcd;
wire [10:0]hcnt;
wire [10:0]vcnt;
rgb_to_ycbcr rgb_to_ycbcr_inst
(
	.clk(clk) ,	// input  clk_sig
	.rst_n(rst_n),
	.i_r_8b({3'b0,lcd_data[15:11]}) ,	// input [7:0] i_r_8b_sig
	.i_g_8b({2'b0,lcd_data[10:5]}) ,	// input [7:0] i_g_8b_sig
	.i_b_8b({3'b0,lcd_data[4:0]}) ,	// input [7:0] i_b_8b_sig
	.i_h_sync(hs) ,	// input  i_h_sync_sig
	.i_v_sync(vs) ,	// input  i_v_sync_sig
	.i_data_en(en) ,	// input  i_data_en_sig
	.hcnt(hcnt) ,	// input [10:0] hcnt_sig
	.vcnt(vcnt) ,	// input [10:0] vcnt_sig
	.o_y_8b(lcd_rgb) ,	// output [7:0] o_y_8b_sig
	.o_cb_8b() ,	// output [7:0] o_cb_8b_sig
	.o_cr_8b() ,	// output [7:0] o_cr_8b_sig
	.o_h_sync(lcd_hs) ,	// output  o_h_sync_sig
	.o_v_sync(lcd_vs) ,	// output  o_v_sync_sig
	.o_data_en()	// output  o_data_en_sig
);
assign	lcd_request	= 1'b1;
assign	lcd_framesync = 1'b1;

//assign lcd_rgb={Y[7:3],Y[7:2],Y[7:3]};

//-------------------------------------
//lcd_driver u_lcd_driver
//(
//	//global clock
//	.clk			(clk),		
//	.rst_n			(rst_n), 
//	 
//	 //lcd interface
//	.lcd_dclk		(lcd_dclk),
//	.lcd_blank		(lcd_blank),
//	.lcd_sync		(lcd_sync),		    	
//	.lcd_hs			(hs),		
//	.lcd_vs			(vs),
//	.lcd_en			(lcd_en),		
//	.lcd_rgb		(lcd),	
//	.hcnt			(hcnt),
//	.vcnt			(vcnt),
//	
//	//user interface
//	.lcd_request	(lcd_request),
//	.lcd_framesync	(lcd_framesync),
//	.lcd_data		(lcd_data),	
//	.lcd_xpos		(lcd_xpos),	
//	.lcd_ypos		(lcd_ypos),
//);

endmodule


