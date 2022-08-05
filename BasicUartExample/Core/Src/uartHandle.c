#include "uartHandle.h"
#include "main.h"


uint8_t 	uartRxData;
//uint8_t 	UART_RawData[BUFFER_SIZE];
uint8_t 	UART_RawDataCnt = 0;
uint8_t 	rxEventCallbackFlag = RESET;
uint8_t		uartReceiveComplatedFlag = RESET;
uint8_t		uartTimeoutcounter = 0;

uartMessage ReceiveMessage;
uartMessage TransmitMessage;

uartMessage *HandleMsg; // gelen mesaji almak icin paket




uint8_t communicationHandler(uartMessage *msg, uint8_t dataLenght)
{
	uartMessage *_msg; //mesaj gondermek icin paket yapildi
	uint8_t 	dataBuffer[BUFFER_SIZE];
	_msg = &TransmitMessage;
	/* Clearing Buffer */
	memset(dataBuffer, 0, BUFFER_SIZE);
	/* TransmitMessage->Buffer should be clear before send packet */
	memset(&_msg->Buffer, 0, BUFFER_SIZE);

	if(checkLrc(msg ,dataLenght) != SUCCEED) return;

	switch(msg->Params.uartMsgSubCommand)
	{

	case SC_Open_Command:

	memset(_msg, 0, BUFFER_SIZE);
	_msg->Params.uartDeviceId    		 = Device_BMS;
	_msg->Params.uartLrcData    		 = 0x00;
	_msg->Params.uartMsgCommand    		 = MC_Setting_Device;
	_msg->Params.uartMsgSubCommand 		 = SC_Open_Command;
	_msg->Params.uartRecvLen			 = 0x40;
	Send_Packet(_msg);
	HAL_Delay(5);

	ledFlag = 1;

		break;


	case SC_Close_Command:

	memset(_msg, 0, BUFFER_SIZE);
	_msg->Params.uartDeviceId    		 = Device_BMS;
	_msg->Params.uartLrcData    		 = 0x00;
	_msg->Params.uartMsgCommand    		 = MC_Setting_Device;
	_msg->Params.uartMsgSubCommand 		 = SC_Close_Command;
	_msg->Params.uartRecvLen			 = 0x40;
	Send_Packet(_msg);
	HAL_Delay(5);

	ledFlag = 0;

		break;

	case SC_Device_Check:

	memset(_msg, 0, BUFFER_SIZE);
	_msg->Params.uartDeviceId    		 = Device_BMS;
	_msg->Params.uartLrcData    		 = 0x00;
	_msg->Params.uartMsgCommand    		 = MC_Setting_Device;
	_msg->Params.uartMsgSubCommand 		 = SC_Device_Check;
	_msg->Params.uartRecvLen			 = 0x40;
	Send_Packet(_msg);
	HAL_Delay(5);

	ledFlag = 0;

		break;
	}

	return 1;

}

uint8_t lrcCompute(uint8_t* p_data , uint8_t size)
{
	uint8_t count = 0;
	uint8_t Lrc = 0;
	for(count = 2; count < size ; count++)
	{
	Lrc = Lrc + p_data[count];
	}
	return Lrc;
}

int8_t checkLrc(uint8_t* p_data , uint8_t size)
{
	uint8_t checklrc = 0;
	checklrc = lrcCompute(p_data , size);
	if(checklrc == p_data[LRC_INDIS])
	{
	return SUCCEED;
	}
	return FAILED;
}

void Send_Packet(uartMessage *msg)
{
	uint8_t lenght;
	lenght = msg->Params.uartRecvLen;
	msg->Params.uartLrcData = lrcCompute((uint8_t*) msg, lenght);
	HAL_UART_Transmit_IT(&huart1, &msg->Buffer[0], lenght);
}



void uartDataHandler(void)
{
	uint8_t bufferLenght;

	if(uartReceiveComplatedFlag == SET) //alma islemi tammalandi
	{
		uartReceiveComplatedFlag = RESET;
		bufferLenght = UART_RawDataCnt;
		HandleMsg = &ReceiveMessage.Buffer;      // alinan paket mesaj paketine yazildi
		if(communicationHandler(HandleMsg, HandleMsg->Params.uartRecvLen) != 1)	//Mesaj paketi cozumleniyor
		{
			/*	}
			Scenerios:	else
			1- invalid Main Func	{
			2- invalid Sub Func		// henuz RX tamamlanmadi !!
			3-	}
		*/
		}
	}
}

void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{

	rxEventCallbackFlag = SET; // data geldi bayragi
	ReceiveMessage.Buffer[UART_RawDataCnt++] = uartRxData; //gelen datayı sıra ile buffera alıyor
	if(UART_RawDataCnt >= BUFFER_SIZE)
	{
		UART_RawDataCnt  = 0;
	}
	HAL_UART_Receive_IT(&huart1 , &uartRxData , 1); // yeni data için kesmeyi tekrar kur
	uartTimeoutcounter = 0; // her data geldiğinde timeouttu sıfırla

}

// This function called in the systick timer
void Uart_Systick_Timer(void)
{
	if(rxEventCallbackFlag == SET)
	{
		if(uartTimeoutcounter++ > UART_TIMEOUT) //uarttımeout kısatılacak test edilip
		{

			rxEventCallbackFlag = RESET; //flagleri temizle
			uartTimeoutcounter = 0;
			uartReceiveComplatedFlag = SET; // data alımı bitti
		}
	}
}

