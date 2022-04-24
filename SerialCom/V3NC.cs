using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCom
{
	public enum V3nc_ClassCode
	{
		Temperature = 0x01,
		SystemPower = 0x05,
		UartResponder = 0x06,
		DeviceVersion = 0x07,
		Misc = 0x08
	}

	public enum V3nc_OpCode
	{
		GetBmcAreaTemp = 0x01,
		GetPchAreaTemp = 0x02,
		GetCpu1BuckTemp = 0x03,
		GetCpu1AreaTemp = 0x04,
		GetSsd2Temp = 0x05,
		GetSsd1Temp = 0x06,
		GetCpu0BuckTemp = 0x07,
		GetCpu0AreaTemp = 0x08,
		GetAllTemp = 0x09,

		GetSysPwrOkStatus = 0x01,
		GetSystemPowerState = 0x02,
		SetPowerOff = 0x03,
		SetPowerOn = 0x04,
		SetPowerReset = 0x05,
		SetPowerCycle = 0x06,
		SetSoftwarePowerReset = 0x07,

		GetWhoIsUartResponder = 0x00,
		SetPassUartCmdToBmc = 0x01,
		SetPassUartCmdToCpld = 0x02,

		GetBmcVersion = 0x01,
		GetCpldVersion = 0x02,

		Get7SegmentDebugCode = 0x01,
		SetTriggerRtcReset = 0x02
	}

	public enum V3nc_CmdCode
	{
		GetBmcAreaTemp = ((V3nc_ClassCode.Temperature << 4) | (V3nc_OpCode.GetBmcAreaTemp)),
		GetPchAreaTemp = ((V3nc_ClassCode.Temperature << 4) | (V3nc_OpCode.GetPchAreaTemp)),
		GetCpu1BuckTemp = ((V3nc_ClassCode.Temperature << 4) | (V3nc_OpCode.GetCpu1BuckTemp)),
		GetCpu1AreaTemp = ((V3nc_ClassCode.Temperature << 4) | (V3nc_OpCode.GetCpu1AreaTemp)),
		GetSsd2Temp = ((V3nc_ClassCode.Temperature << 4) | (V3nc_OpCode.GetSsd2Temp)),
		GetSsd1Temp = ((V3nc_ClassCode.Temperature << 4) | (V3nc_OpCode.GetSsd1Temp)),
		GetCpu0BuckTemp = ((V3nc_ClassCode.Temperature << 4) | (V3nc_OpCode.GetCpu0BuckTemp)),
		GetCpu0AreaTemp = ((V3nc_ClassCode.Temperature << 4) | (V3nc_OpCode.GetCpu0AreaTemp)),
		GetAllTemp = ((V3nc_ClassCode.Temperature << 4) | (V3nc_OpCode.GetAllTemp)),

		GetSysPwrOkStatus = ((V3nc_ClassCode.SystemPower << 4) | (V3nc_OpCode.GetSysPwrOkStatus)),
		GetSystemPowerState = ((V3nc_ClassCode.SystemPower << 4) | (V3nc_OpCode.GetSystemPowerState)),
		SetPowerOff = ((V3nc_ClassCode.SystemPower << 4) | (V3nc_OpCode.SetPowerOff)),
		SetPowerOn = ((V3nc_ClassCode.SystemPower << 4) | (V3nc_OpCode.SetPowerOn)),
		SetPowerReset = ((V3nc_ClassCode.SystemPower << 4) | (V3nc_OpCode.SetPowerReset)),
		SetPowerCycle = ((V3nc_ClassCode.SystemPower << 4) | (V3nc_OpCode.SetPowerCycle)),
		SetSoftwarePowerReset = ((V3nc_ClassCode.SystemPower << 4) | (V3nc_OpCode.SetSoftwarePowerReset)),

		GetWhoIsUartResponder = ((V3nc_ClassCode.UartResponder << 4) | (V3nc_OpCode.GetWhoIsUartResponder)),
		SetPassUartCmdToBmc = ((V3nc_ClassCode.UartResponder << 4) | (V3nc_OpCode.SetPassUartCmdToBmc)),
		SetPassUartCmdToCpld = ((V3nc_ClassCode.UartResponder << 4) | (V3nc_OpCode.SetPassUartCmdToCpld)),

		GetBmcVersion = ((V3nc_ClassCode.DeviceVersion << 4) | (V3nc_OpCode.GetBmcVersion)),
		GetCpldVersion = ((V3nc_ClassCode.DeviceVersion << 4) | (V3nc_OpCode.GetCpldVersion)),

		Get7SegmentDebugCode = ((V3nc_ClassCode.Misc << 4) | (V3nc_OpCode.Get7SegmentDebugCode)),
		SetTriggerRtcReset = ((V3nc_ClassCode.Misc << 4) | (V3nc_OpCode.SetTriggerRtcReset)),
	}

	public enum V3nc_Msg_Err
    {
		NoErr = 0,
		Invalid_StartChar = -1,
		Invalid_DirectionChar = -2,
		Invalid_RequestDataLength = -3,
		Invalid_CmdCode = -4,
		Invalid_EndChar = -5,
		Invalid_Checksum = -6,
	}


	struct V3nc_Cmd_s
	{
		public V3nc_CmdCode cmdCode;
		public byte cpldRspDataLength;    /* data length for CPLD rsp, op code(1 byte) is excluded */
		public byte bmcRspDataLength;     /* data length for BMC rsp, op code(1 byte) is excluded */
		public string description;

		public V3nc_Cmd_s(V3nc_CmdCode cmdCode, byte cpldRspDataLength, byte bmcRspDataLength, string description)
        {
			this.cmdCode = cmdCode;
			this.cpldRspDataLength = cpldRspDataLength;
			this.bmcRspDataLength = bmcRspDataLength;
			this.description = description;
        }
	}

	class V3NC
    {
		public const int MAX_TX_PKT_SIZE = 16;
		public const int MAX_RX_PKT_SIZE = 16;
		public const byte START_CHAR = 0x7B;
		public const byte END_CHAR = 0x7C;
		public const byte REQUEST_DATA_SIZE = 1;
		public const byte DIR_REQUEST = 0x01;
		public const byte DIR_RESPONSE = 0x02;

		public byte[] txBuf = new byte[MAX_TX_PKT_SIZE];
		public byte[] rxBuf = new byte[MAX_RX_PKT_SIZE];

		private V3nc_Cmd_s[] v3ncCmdTable =
		{
			// ref to file "V3NC SW Design Spec_UART_20201126.pdf"
			/* code is composed of class_code and op_code */
			/* cmd_code,		  op,  cpld, bmc, description */
			new V3nc_Cmd_s ( V3nc_CmdCode.GetBmcAreaTemp, 0x01, 0x01, "Get BMC Area Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetPchAreaTemp, 0x01, 0x01, "Get PCH Area Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetCpu1BuckTemp, 0x01, 0x01, "Get CPU1 Buck Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetCpu1AreaTemp, 0x01, 0x01, "Get CPU1 Area Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetSsd2Temp, 0x00, 0x01, "Get SSD2 Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetSsd1Temp, 0x00, 0x01, "Get SSD1 Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetCpu0BuckTemp, 0x00, 0x01, "Get CPU0 Buck Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetCpu0AreaTemp, 0x00, 0x01, "Get CPU0 Area Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetAllTemp, 0x00, 0x08, "Get All Temp" ),
			/* cmd_code,			op,  cpld, bmc, description */
			new V3nc_Cmd_s ( V3nc_CmdCode.GetSysPwrOkStatus, 0x01, 0x00, "Get SYS_PWROK status" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetSystemPowerState, 0x00, 0x01, "Get System Power state" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPowerOff, 0x00, 0x01, "Set Power Off" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPowerOn, 0x00, 0x01, "Set Power On" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPowerReset, 0x00, 0x01, "Set Power Reset" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPowerCycle, 0x00, 0x01, "Set Power Cycle" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetSoftwarePowerReset, 0x00, 0x01, "Set Software Power Reset" ),
			/* cmd_code,			op,  cpld, bmc, description */
			new V3nc_Cmd_s ( V3nc_CmdCode.GetWhoIsUartResponder, 0x00, 0x01, "Get who is UART responder" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPassUartCmdToBmc, 0x01, 0x00, "Set pass UART cmd to BMC" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPassUartCmdToCpld, 0x00, 0x01, "Set pass UART cmd to CPLD" ),
			/* cmd_code,			op,  cpld, bmc, description */
			new V3nc_Cmd_s ( V3nc_CmdCode.GetBmcVersion, 0x00, 0x05, "Get BMC version" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetCpldVersion, 0x02, 0x00, "Get CPLD version" ),
			/* class_code, 		  op,  cpld, bmc, description */
			new V3nc_Cmd_s ( V3nc_CmdCode.Get7SegmentDebugCode, 0x02, 0x02, "Get 7-segment LED debug code" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetTriggerRtcReset, 0x01, 0x01, "Set Trigger RTC reset" ),
		};

		private V3nc_CmdCode GetV3nc_CmdCode(byte classCode, byte opCode)
        {
			V3nc_CmdCode cmdCode;

			cmdCode = (V3nc_CmdCode)((((int)classCode << 4) & 0xf0) | ((int)opCode & 0x0f));

			return cmdCode;
		}

		private V3nc_ClassCode GetClassCode(V3nc_CmdCode cmd)
        {
			V3nc_ClassCode classCode;

			classCode = (V3nc_ClassCode)(((int)cmd >> 4) & 0x0F);

			return classCode;
		}

		private V3nc_OpCode GetOpCode(V3nc_CmdCode cmd)
		{
			V3nc_OpCode opCode;

			opCode = (V3nc_OpCode)((int)cmd & 0x0F);

			return opCode;
		}

		private byte CalculateChecksum(byte[] bytes, int size)
        {
			byte cs = 0;

			for (int i = 0; i < size; i++)
            {
				cs += bytes[i];
            }

			return cs;
        }


		public V3nc_Msg_Err ValidateV3npMsg(byte[] rxBytes)
        {
			V3nc_Msg_Err rval = V3nc_Msg_Err.NoErr;
			V3nc_ClassCode classCode;
			V3nc_OpCode opCode;
			V3nc_CmdCode cmdCode;
			int rxDataLength = 0;
			int index = -1;
			byte rxChecksum = 0;
			byte calChecksum = 0;

			// start char, 0
			if (rxBytes[0] != START_CHAR)
            {
				rval = V3nc_Msg_Err.Invalid_StartChar;
				goto validate_done;
			}

			// direction, 1
			if (rxBytes[1] != DIR_REQUEST)
            {
				rval = V3nc_Msg_Err.Invalid_DirectionChar;
				goto validate_done;
			}

			// rx data length
			if (rxBytes[2] != REQUEST_DATA_SIZE)
            {
				rval = V3nc_Msg_Err.Invalid_RequestDataLength;
				goto validate_done;

			}

			// class code, 3
			// op code, 4
			cmdCode = GetV3nc_CmdCode(rxBytes[3], rxBytes[4]);

			for (int i = 0; i < v3ncCmdTable.Length; i++)
            {
				if (cmdCode == v3ncCmdTable[i].cmdCode)
                {
					index = i;
					break;
                }
			}
			if (index < 0)
            {
				rval = V3nc_Msg_Err.Invalid_CmdCode;
				goto validate_done;
			}

			if (rxBytes[6] != END_CHAR)
            {
				rval = V3nc_Msg_Err.Invalid_EndChar;
				goto validate_done;
            }

			rxChecksum = rxBytes[5];
			var msgBytes = rxBytes.Where(w => w != rxBytes[0]).ToArray();
			calChecksum = CalculateChecksum(msgBytes, 4);

			if (rxChecksum != calChecksum)
            {
				rval = V3nc_Msg_Err.Invalid_Checksum;
				goto validate_done;
			}

			validate_done:

				return rval;
        }

	}
}
