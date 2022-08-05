/*
 * uartHandle.h
 *
 *  Created on: Jan 30, 2022
 *      Author: oktay
 */

#ifndef UARTHANDLE_H_
#define UARTHANDLE_H_

#include "stdbool.h"
#include "stdio.h"
#include "stdint.h"
#include "string.h"

#define BUFFER_SIZE 64
#define DATA_BUFFER_SIZE 56

#define UART_TIMEOUT	100

#define LRC_INDIS 		1

#define FAILED  		0
#define SUCCEED  		1
#define ON_GOING  		2
#define EQUAL			1

enum Device_Id
{
	Device_BMS 		   = 0xF1,
	Device_Ecu 		   = 0xF2,
	Device_MotorDriver = 0xF3,
	Device_PC		   = 0xFF,
	//add other Device
};

enum Main_Command
{

    MC_Setting_Device = 1,
    //add other Main Command
};

enum Sub_Command
{

    /** MC_Setting_Device */
    SC_Open_Command  = 1,
    SC_Close_Command = 2,
    SC_Device_Check  = 3,
    SC_FW_Update     = 4,

    //add other Sub Command for Main Command

};


typedef union
{
	struct
	{
		/* UART Protocol variables */
		uint8_t uartDeviceId;									     //!< 0 Device ID
		uint8_t uartLrcData;										 //!< 1 LRC
		uint8_t uartMsgCommand;									     //!< 2 Main command
		uint8_t uartMsgSubCommand;									 //!< 3 Sub  command
		uint8_t uartRecvLen;										 //!< 4 Length
		uint8_t uartReserve[3]; 									 //!< 5 6 7 Reserve

		uint8_t uartPackageData[DATA_BUFFER_SIZE];   				 //!< 8+ message data
		/**/

	}Params;

	uint8_t Buffer[BUFFER_SIZE];

}uartMessage;


extern uint8_t 	 	uartRxData;
extern uint8_t 	 	UART_RawData[BUFFER_SIZE];
extern uint8_t 	 	UART_RawDataCnt;
extern uint8_t 	   	rxEventCallbackFlag;
extern uint8_t		uartReceiveComplatedFlag;
extern uint8_t		uartTimeoutcounter;

void	uartDataHandler(void);
void    Send_Packet(uartMessage *msg);
uint8_t	communicationHandler(uartMessage *msg, uint8_t dataLenght);
void Uart_Systick_Timer(void);
int8_t checkLrc(uint8_t* p_data , uint8_t size);

#endif /* UARTHANDLE_H_ */
