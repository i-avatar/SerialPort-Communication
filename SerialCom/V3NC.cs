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
		SetSwitchHostUart1RouteIo2 = 0x03,
		SetSwitchHostUart1RouteIo1 = 0x04,

		GetBmcVersion = 0x01,
		GetCpldVersion = 0x02,
		GetUcdVersion = 0x03,
		GetBiosVersion = 0x04,

		Get7SegmentDebugCode = 0x01,
		SetTriggerRtcReset = 0x02,
		DisableAutoPowerOn = 0x03,
		EnableAutoPowerOn = 0x04
	}

	public enum V3nc_CmdCode
	{
		GetBmcAreaTemp = ((V3nc_ClassCode.Temperature << 8) | (V3nc_OpCode.GetBmcAreaTemp)),
		GetPchAreaTemp = ((V3nc_ClassCode.Temperature << 8) | (V3nc_OpCode.GetPchAreaTemp)),
		GetCpu1BuckTemp = ((V3nc_ClassCode.Temperature << 8) | (V3nc_OpCode.GetCpu1BuckTemp)),
		GetCpu1AreaTemp = ((V3nc_ClassCode.Temperature << 8) | (V3nc_OpCode.GetCpu1AreaTemp)),
		GetSsd2Temp = ((V3nc_ClassCode.Temperature << 8) | (V3nc_OpCode.GetSsd2Temp)),
		GetSsd1Temp = ((V3nc_ClassCode.Temperature << 8) | (V3nc_OpCode.GetSsd1Temp)),
		GetCpu0BuckTemp = ((V3nc_ClassCode.Temperature << 8) | (V3nc_OpCode.GetCpu0BuckTemp)),
		GetCpu0AreaTemp = ((V3nc_ClassCode.Temperature << 8) | (V3nc_OpCode.GetCpu0AreaTemp)),
		GetAllTemp = ((V3nc_ClassCode.Temperature << 8) | (V3nc_OpCode.GetAllTemp)),

		GetSysPwrOkStatus = ((V3nc_ClassCode.SystemPower << 8) | (V3nc_OpCode.GetSysPwrOkStatus)),
		GetSystemPowerState = ((V3nc_ClassCode.SystemPower << 8) | (V3nc_OpCode.GetSystemPowerState)),
		SetPowerOff = ((V3nc_ClassCode.SystemPower << 8) | (V3nc_OpCode.SetPowerOff)),
		SetPowerOn = ((V3nc_ClassCode.SystemPower << 8) | (V3nc_OpCode.SetPowerOn)),
		SetPowerReset = ((V3nc_ClassCode.SystemPower << 8) | (V3nc_OpCode.SetPowerReset)),
		SetPowerCycle = ((V3nc_ClassCode.SystemPower << 8) | (V3nc_OpCode.SetPowerCycle)),
		SetSoftwarePowerReset = ((V3nc_ClassCode.SystemPower << 8) | (V3nc_OpCode.SetSoftwarePowerReset)),

		GetWhoIsUartResponder = ((V3nc_ClassCode.UartResponder << 8) | (V3nc_OpCode.GetWhoIsUartResponder)),
		SetPassUartCmdToBmc = ((V3nc_ClassCode.UartResponder << 8) | (V3nc_OpCode.SetPassUartCmdToBmc)),
		SetPassUartCmdToCpld = ((V3nc_ClassCode.UartResponder << 8) | (V3nc_OpCode.SetPassUartCmdToCpld)),
		SetSwitchHostUart1RouteIo2 = ((V3nc_ClassCode.UartResponder << 8) | (V3nc_OpCode.SetSwitchHostUart1RouteIo2)),
		SetSwitchHostUart1RouteIo1 = ((V3nc_ClassCode.UartResponder << 8) | (V3nc_OpCode.SetSwitchHostUart1RouteIo1)),

		GetBmcVersion = ((V3nc_ClassCode.DeviceVersion << 8) | (V3nc_OpCode.GetBmcVersion)),
		GetCpldVersion = ((V3nc_ClassCode.DeviceVersion << 8) | (V3nc_OpCode.GetCpldVersion)),
		GetUcdVersion = ((V3nc_ClassCode.DeviceVersion << 8) | (V3nc_OpCode.GetUcdVersion)),
		GetBiosVersion = ((V3nc_ClassCode.DeviceVersion << 8) | (V3nc_OpCode.GetBiosVersion)),

		Get7SegmentDebugCode = ((V3nc_ClassCode.Misc << 8) | (V3nc_OpCode.Get7SegmentDebugCode)),
		SetTriggerRtcReset = ((V3nc_ClassCode.Misc << 8) | (V3nc_OpCode.SetTriggerRtcReset)),
		DisableAutoPowerOn = ((V3nc_ClassCode.Misc << 8) | (V3nc_OpCode.DisableAutoPowerOn)),
		EnableAutoPowerOn = ((V3nc_ClassCode.Misc << 8) | (V3nc_OpCode.EnableAutoPowerOn)),
	}

	public enum V3nc_UartResponder
	{
		Cpld = 0x01,
		Bmc = 0x02
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

	public class V3nc_BoardTemp_s
	{
		public sbyte BmcArea;
		public sbyte PchArea;
		public sbyte Cpu1Buck;
		public sbyte Cpu1Area;
		public sbyte Ssd2;
		public sbyte Ssd1;
		public sbyte Cpu0Buck;
		public sbyte Cpu0Area;
	}

	public class BmcVersion_s
	{
		public byte Major;
		public byte Minor;
		public byte AuxVerPart0;
		public byte AuxVerPart1;
		public byte AuxVerPart2;
	}

	public class CpldVersion_s
	{
		public byte Version;
	}

	public class DebugCode_s
	{
		public byte DebugCode;
		public byte Responder;
	}

	struct V3nc_Cmd_s
	{
		public V3nc_CmdCode CmdCode;
		public byte CpldRspDataLength;    /* data length for CPLD rsp, op code(1 byte) is excluded */
		public byte BmcRspDataLength;     /* data length for BMC rsp, op code(1 byte) is excluded */
		public string Description;

		public V3nc_Cmd_s(V3nc_CmdCode cmdCode, byte cpldRspDataLength, byte bmcRspDataLength, string description)
        {
			this.CmdCode = cmdCode;
			this.CpldRspDataLength = cpldRspDataLength;
			this.BmcRspDataLength = bmcRspDataLength;
			this.Description = description;
        }
	}

	public class V3ncBoard
	{
		public V3nc_BoardTemp_s Temp { get; set; }
		public byte SystemPwrOkStatus { get; set; }
		public byte SystemPowerState { get; set; }
		public byte CurrentUartResponder { get; set; }
		public CpldVersion_s CpldVer { get; set; }
		public BmcVersion_s BmcVer { get; set; }
		public byte[] DebugCode = new byte[2];

		public V3ncBoard()
        {
			Temp = new V3nc_BoardTemp_s();
			CpldVer = new CpldVersion_s();
			BmcVer = new BmcVersion_s();
        }
	}

	public class V3NC
    {
		static V3NC v3nc;

		public static V3NC Instance
        {
            get
            {
				if (v3nc == null)
					v3nc = new V3NC();
				return v3nc;
            }
        }

		protected V3NC() { }

		public const int MAX_TX_PKT_SIZE = 16;
		public const int MAX_RX_PKT_SIZE = 16;
		public const byte START_CHAR = 0x7B;
		public const byte END_CHAR = 0x7C;
		public const byte REQUEST_DATA_SIZE = 1;
		public const byte DIR_REQUEST = 0x01;
		public const byte DIR_RESPONSE = 0x02;
		public const byte BASE_RESPONSE_MSG_SIZE = 0x04;
		public const byte START_AND_END_CHAR_SIZE = 0x02;

		public V3nc_UartResponder currentResponder = V3nc_UartResponder.Cpld;
		public byte[] txBuf = new byte[MAX_TX_PKT_SIZE];
		public byte[] rxBuf = new byte[MAX_RX_PKT_SIZE];
		//public V3ncBoard Board;

		private V3nc_Cmd_s[] v3ncCmdTable =
		{
			// ref to file "V3NC SW Design Spec_UART_20201126.pdf"
			/* code is composed of class_code and op_code */
			/*				cmd_code,					cpld, bmc, description */
			new V3nc_Cmd_s ( V3nc_CmdCode.GetBmcAreaTemp, 0x01, 0x01, "Get BMC Area Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetPchAreaTemp, 0x01, 0x01, "Get PCH Area Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetCpu1BuckTemp, 0x01, 0x01, "Get CPU1 Buck Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetCpu1AreaTemp, 0x01, 0x01, "Get CPU1 Area Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetSsd2Temp, 0x00, 0x01, "Get SSD2 Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetSsd1Temp, 0x00, 0x01, "Get SSD1 Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetCpu0BuckTemp, 0x00, 0x01, "Get CPU0 Buck Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetCpu0AreaTemp, 0x00, 0x01, "Get CPU0 Area Temp" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetAllTemp, 0x00, 0x08, "Get All Temp" ),
			/*				cmd_code,						cpld, bmc, description */
			new V3nc_Cmd_s ( V3nc_CmdCode.GetSysPwrOkStatus, 0x01, 0x00, "Get SYS_PWROK status" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetSystemPowerState, 0x00, 0x01, "Get System Power state" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPowerOff, 0x00, 0x01, "Set Power Off" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPowerOn, 0x00, 0x01, "Set Power On" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPowerReset, 0x00, 0x01, "Set Power Reset" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPowerCycle, 0x00, 0x01, "Set Power Cycle" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetSoftwarePowerReset, 0x00, 0x01, "Set Software Power Reset" ),
			/*				cmd_code,							cpld, bmc, description */
			new V3nc_Cmd_s ( V3nc_CmdCode.GetWhoIsUartResponder, 0x00, 0x01, "Get who is UART responder" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPassUartCmdToBmc, 0x01, 0x00, "Set pass UART cmd to BMC" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetPassUartCmdToCpld, 0x00, 0x01, "Set pass UART cmd to CPLD" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetSwitchHostUart1RouteIo2, 0x00, 0x01, "Set Switch Host Uart1 Route Io2" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetSwitchHostUart1RouteIo2, 0x00, 0x01, "Set Switch Host Uart1 Route Io1" ),
			/*				cmd_code,					cpld, bmc, description */
			new V3nc_Cmd_s ( V3nc_CmdCode.GetBmcVersion, 0x00, 0x05, "Get BMC version" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetCpldVersion, 0x02, 0x00, "Get CPLD version" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetUcdVersion, 0x00, 0x08, "Get UCD version" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.GetBiosVersion, 0x00, 0x08, "Get BIOS version" ),
			/*				cmd_code,							cpld, bmc, description */
			new V3nc_Cmd_s ( V3nc_CmdCode.Get7SegmentDebugCode, 0x02, 0x02, "Get 7-segment LED debug code" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.SetTriggerRtcReset, 0x01, 0x01, "Set Trigger RTC reset" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.DisableAutoPowerOn, 0x01, 0x00, "Disable Auto Power On" ),
			new V3nc_Cmd_s ( V3nc_CmdCode.EnableAutoPowerOn, 0x01, 0x00, "Enable Auto Power On" ),
		};

		private int SearchCmdCodeIndex(V3nc_CmdCode cmdCode)
        {
			int index = -1;

			for (int i = 0; i < v3ncCmdTable.Length; i++)
            {
				if (v3ncCmdTable[i].CmdCode == cmdCode)
                {
					index = i;
					break;
                }
            }

			return index;
        }

		private V3nc_CmdCode GetV3nc_CmdCode(byte classCode, byte opCode)
        {
			V3nc_CmdCode cmdCode;

			cmdCode = (V3nc_CmdCode)((((int)classCode << 8) & 0xFF00) | ((int)opCode & 0x00FF));

			return cmdCode;
		}

		private V3nc_ClassCode GetClassCode(V3nc_CmdCode cmd)
        {
			V3nc_ClassCode classCode;

			classCode = (V3nc_ClassCode)(((int)cmd >> 8) & 0xFF);

			return classCode;
		}

		private V3nc_OpCode GetOpCode(V3nc_CmdCode cmd)
		{
			V3nc_OpCode opCode;

			opCode = (V3nc_OpCode)((int)cmd & 0xFF);

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

		public V3nc_CmdCode GetV3npMsgCmdCode(byte[] rxBytes)
        {
			V3nc_CmdCode cmdCode;

			cmdCode = GetV3nc_CmdCode(rxBytes[3], rxBytes[4]);

			return cmdCode;
		}

		public V3nc_Msg_Err ValidateV3npMsg(byte[] rxBytes)
        {
			V3nc_Msg_Err rval = V3nc_Msg_Err.NoErr;
			V3nc_ClassCode classCode;
			V3nc_OpCode opCode;
			V3nc_CmdCode cmdCode;
			byte rxDataLength = 0;
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
			rxDataLength = rxBytes[2];
			if (rxDataLength != REQUEST_DATA_SIZE)
            {
				rval = V3nc_Msg_Err.Invalid_RequestDataLength;
				goto validate_done;
			}
			// class code, 3, op code, 4
			cmdCode = GetV3nc_CmdCode(rxBytes[3], rxBytes[4]);

			for (int i = 0; i < v3ncCmdTable.Length; i++)
            {
				if (cmdCode == v3ncCmdTable[i].CmdCode)
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

		public byte[] SendV3npRsp(V3ncBoard board, V3nc_CmdCode cmdCode, V3nc_UartResponder responder)
        {
			byte rval = 0;
			byte checksum = 0x00;
			byte[] txBuffer = null;
			int lastTxIndex = 0;
			byte[] msgBuffer = new byte[MAX_TX_PKT_SIZE];
			int index = -1;
			byte dataLength = 0;
			int i = 0;

			index = SearchCmdCodeIndex(cmdCode);
			if (index < 0)
				return null;

			if (responder == V3nc_UartResponder.Bmc)
				dataLength = v3ncCmdTable[index].BmcRspDataLength;
			else if (responder == V3nc_UartResponder.Cpld)
				dataLength = v3ncCmdTable[index].CpldRspDataLength;

			// FIXME: how to send out if uart responder do not support the cmd?
			if (dataLength <= 0)
				return null;

			i = 0;
			msgBuffer[i++] = DIR_RESPONSE;
			msgBuffer[i++] = dataLength;
			msgBuffer[i++] = (byte)GetClassCode(cmdCode);
			msgBuffer[i++] = (byte)GetOpCode(cmdCode);
			if (cmdCode == V3nc_CmdCode.GetBmcAreaTemp)
			{
				msgBuffer[i++] = (byte)board.Temp.BmcArea;
			}
			else if (cmdCode == V3nc_CmdCode.GetPchAreaTemp)
			{
				msgBuffer[i++] = (byte)board.Temp.PchArea;
			}
			else if (cmdCode == V3nc_CmdCode.GetCpu1BuckTemp)
			{
				msgBuffer[i++] = (byte)board.Temp.Cpu1Buck;
			}
			else if (cmdCode == V3nc_CmdCode.GetCpu1AreaTemp)
            {
				msgBuffer[i++] = (byte)board.Temp.Cpu1Area;
			}
			else if (cmdCode == V3nc_CmdCode.GetSsd2Temp)
			{
				msgBuffer[i++] = (byte)board.Temp.Ssd2;
			}
			else if (cmdCode == V3nc_CmdCode.GetSsd1Temp)
			{
				msgBuffer[i++] = (byte)board.Temp.Ssd1;
			}
			else if (cmdCode == V3nc_CmdCode.GetCpu0BuckTemp)
			{
				msgBuffer[i++] = (byte)board.Temp.Cpu0Buck;
			}
			else if (cmdCode == V3nc_CmdCode.GetCpu0AreaTemp)
			{
				msgBuffer[i++] = (byte)board.Temp.Cpu0Area;
			}
			else if (cmdCode == V3nc_CmdCode.GetAllTemp)
            {
				msgBuffer[i++] = (byte)board.Temp.BmcArea;
				msgBuffer[i++] = (byte)board.Temp.PchArea;
				msgBuffer[i++] = (byte)board.Temp.Cpu1Buck;
				msgBuffer[i++] = (byte)board.Temp.Cpu1Area;
				msgBuffer[i++] = (byte)board.Temp.Ssd2;
				msgBuffer[i++] = (byte)board.Temp.Ssd1;
				msgBuffer[i++] = (byte)board.Temp.Cpu0Buck;
				msgBuffer[i++] = (byte)board.Temp.Cpu0Area;
			}
			else if (cmdCode == V3nc_CmdCode.GetSysPwrOkStatus)
            {
				msgBuffer[i++] = (byte)board.SystemPwrOkStatus;
			}
			else if (cmdCode == V3nc_CmdCode.GetSystemPowerState)
            {
				msgBuffer[i++] = (byte)board.SystemPowerState;
			}
			else if ((cmdCode == V3nc_CmdCode.SetPowerOff) ||
					 (cmdCode == V3nc_CmdCode.SetPowerOn) ||
					 (cmdCode == V3nc_CmdCode.SetPowerReset) ||
					 (cmdCode == V3nc_CmdCode.SetPowerCycle) ||
					 (cmdCode == V3nc_CmdCode.SetSoftwarePowerReset))
            {
				// FIXME: check current state to set the value,
				// here to set hard code 0x01 for test
				msgBuffer[i++] = (byte)0x01;
			}
			else if (cmdCode == V3nc_CmdCode.GetWhoIsUartResponder)
            {
				msgBuffer[i++] = (byte)board.CurrentUartResponder;
			}
			else if (cmdCode == V3nc_CmdCode.SetPassUartCmdToBmc)
            {
				if (board.CurrentUartResponder == (byte)V3nc_UartResponder.Cpld)
                {
					board.CurrentUartResponder = (byte)V3nc_UartResponder.Bmc;
					msgBuffer[i++] = (byte)0x01;
				}
            }
			else if (cmdCode == V3nc_CmdCode.SetPassUartCmdToCpld)
			{
				if (board.CurrentUartResponder == (byte)V3nc_UartResponder.Bmc)
				{
					board.CurrentUartResponder = (byte)V3nc_UartResponder.Cpld;
					msgBuffer[i++] = (byte)0x01;
				}
			}

			checksum = CalculateChecksum(msgBuffer, (dataLength + BASE_RESPONSE_MSG_SIZE));
			msgBuffer[i++] = checksum;
			lastTxIndex = i;

			txBuffer = new byte[lastTxIndex + START_AND_END_CHAR_SIZE];

			txBuffer[0] = START_CHAR;
			for (int j = 0; j < lastTxIndex; j++)
			{
				txBuffer[j + 1] = msgBuffer[j];
			}
			txBuffer[txBuffer.Length - 1] = END_CHAR;

			return txBuffer;
        }
	}
}
