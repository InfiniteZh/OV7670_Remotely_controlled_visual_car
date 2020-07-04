module bus_c1
#(
	parameter N = 32 		//pwm bit width 
)
(	
	input [N-1:0] duty,
	input  clk,
	input  rst_n,
	input  [7:0] signal,
	output [7:0] pwm_out
);

reg [1:0] in_c;
wire [N-1:0] period;
reg [N-1:0] duty1;
reg [N-1:0] duty2;
reg [N-1:0] duty3;
reg [N-1:0] duty4;

always @ (posedge clk or negedge rst_n)
begin
	if(rst_n == 1'b0)begin
		in_c <= 3;
		duty1 <= 32'h3fff_ffff;
		duty2 <= 32'h3fff_ffff;
		duty3 <= 32'h3fff_ffff;
		duty4 <= 32'h3fff_ffff;
	end
	else
		case(signal)
		8'h1A: 
		begin
			in_c <= 0;			//前进
			duty1 <= duty;
			duty2 <= duty;
			duty3 <= duty;
			duty4 <= duty;
		end	
		8'h2A: 
		begin
			in_c <= 3;			//停止
			duty1 <= duty;
			duty2 <= duty;
			duty3 <= duty;
			duty4 <= duty;
		end
		8'h3A: 
		begin
			in_c <= 1;			//左转
			duty1 <= 32'hFFFF;
			duty2 <= 32'hFFFF;
			duty3 <= 32'hFFFF;
			duty4 <= 32'hFFFF;
		end
		8'h4A: 
		begin
			in_c <= 2;			//右转
			duty1 <= 32'hFFFF;
			duty2 <= 32'hFFFF;
			duty3 <= 32'hFFFF;
			duty4 <= 32'hFFFF;
		end
		default:
		begin
			in_c <= 3;		//停止
			duty1 <= duty;
			duty2 <= duty;
			duty3 <= duty;
			duty4 <= duty;
		end
		endcase
end

assign period = 32'd85899;   //1KHz驱动电机效果最好 period = 1Khz*2^32/50MHz

bus_direction#
(
	.N(N)
)
bus_direction_m0(
	.clk		(clk),
	.rst_n	(rst_n),
	.in_c    (in_c),
	.period	(period),
	.duty1	(duty1),
	.duty2	(duty2),
	.duty3	(duty3),
	.duty4	(duty4),
	.pwm_out (pwm_out)
);

endmodule
