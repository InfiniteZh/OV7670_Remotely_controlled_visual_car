module selecct
#(
	parameter N = 32 		//pwm bit width 
)
(
input clk,
input rdsig,
input right1,
input left1,
input stright1,
input [7:0]data,
output reg [N-1:0]duty,
output reg right,
output reg left,
output reg stright,
input [2:0]direction,
input rst_n
);
reg [1:0]mode;
always @(posedge clk or negedge rst_n) begin
	if (!rst_n) begin
		// reset
		mode <= 0;
	end
	else if (data == 49) begin
		mode <= 1;
	end
	else if (data == 50) begin
		mode <= 2;
	end
	else if (data == 51) begin
		mode <= 3;
	end
end

always @(posedge clk or negedge rst_n) begin
	if (!rst_n) begin
		// reset
		right <= 0;
		left <= 0;
		stright <= 0;
	end
	else if (mode == 1) begin
		if(data == 87)
		begin
			stright <= 1;
			left <= 0;
			right <= 0;
		end
		else if (data == 65)
		begin
			stright <= 0;
			left <= 1;
			right <= 0;
		end
		else if (data == 68)
		begin
			stright <= 0;
			left <= 0;
			right <= 1;
		end
		else if(data == 83)
		begin 
			stright <= 0;
			left <= 0;
			right <= 0;
		end

	end
	else if (mode == 2) begin
		stright <= stright1;
		left <= left1;
		right <= right1;
	end
	else if(mode==3) begin
		stright <= direction[1];
		left <= direction[2];
		right <= direction[0];
	end
end


always @(posedge clk or negedge rst_n) begin
	if (!rst_n) begin
		// reset
		duty<=0;
	end
	else if(data == 72)
	begin
		duty <= 32'd1288490188;
	end
	else if(data == 76)
	begin
		duty <= 32'd2791728742;
	end
	else if(data == 78)
	begin
		duty <= 32'd2147483648;
	end
end
endmodule
