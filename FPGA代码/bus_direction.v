`timescale 10ns/1ps

module bus_direction
#(
	parameter N = 32 		//pwm bit width 
)
(
		input clk,
		input rst_n,
		input [1:0] in_c,
		input [N-1:0] period,
		input [N-1:0] duty1,
		input [N-1:0] duty2,
		input [N-1:0] duty3,
		input [N-1:0] duty4,
		output[7:0] pwm_out
);

wire front_left;
wire front_right;
wire back_left;
wire back_right;
reg[N-1:0] period_r;
reg[N-1:0] duty1_r;
reg[N-1:0] duty2_r;
reg[N-1:0] duty3_r;
reg[N-1:0] duty4_r;
reg[7:0] pwm_out_r;

always@(posedge clk or negedge rst_n)
begin
	if(rst_n == 1'b0)
	begin
		period_r <= period;
		duty1_r <= duty1;
		duty2_r <= duty2;
		duty3_r <= duty3;
		duty4_r <= duty4;
	end
	else
	begin
		period_r <= period;	       //32'd85899 PWM频率1KHz
		duty1_r <= duty1;
		duty2_r <= duty2;
		duty3_r <= duty3;
		duty4_r <= duty4;    			
	end
end

always@(posedge clk)
begin
		case(in_c)
		0: pwm_out_r <= {			//前进
				  front_left,
				  1'b0,
				  back_left,
				  1'b0,
				  front_right,
				  1'b0,
				  back_right,
				  1'b0
				 };
		1:pwm_out_r <= {        //左转
				  1'b0,
				  front_left,
			     1'b0,
			     back_left,
			     front_right,
			     1'b0,
			     back_right,
			     1'b0
				 };
		2:pwm_out_r <= {			//右转
				  front_left,
			     1'b0,
			     back_left,
			     1'b0,
			     1'b0,
			     front_right,
			     1'b0,
			     back_right
				 };
		3:pwm_out_r <= {			//停止
				  1'b0,
			     1'b0,
			     1'b0,
			     1'b0,
			     1'b0,
			     1'b0,
			     1'b0,
			     1'b0
				 };
		default:pwm_out_r <= {			//停止
				  1'b0,
			     1'b0,
			     1'b0,
			     1'b0,
			     1'b0,
			     1'b0,
			     1'b0,
			     1'b0
				 };
		endcase
end

assign pwm_out = pwm_out_r;

bus_pwm#
(
	.N(N)
)
bus_pwm_m0(
	.clk		(clk),
	.rst		(~rst_n),
	.period	(period_r),
	.duty		(duty1_r),
	.pwm_out	(front_left)
);

bus_pwm#
(
	.N(N)
)
bus_pwm_m1(
	.clk		(clk),
	.rst		(~rst_n),
	.period	(period_r),
	.duty		(duty2_r),
	.pwm_out	(front_right)
);

bus_pwm#
(
	.N(N)
)
bus_pwm_m2(
	.clk		(clk),
	.rst		(~rst_n),
	.period	(period_r),
	.duty		(duty3_r),
	.pwm_out	(back_left)
);

bus_pwm#
(
	.N(N)
)
bus_pwm_m3(
	.clk		(clk),
	.rst		(~rst_n),
	.period	(period_r),
	.duty		(duty4_r),
	.pwm_out	(back_right)
);


endmodule