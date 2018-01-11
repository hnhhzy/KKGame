#include "KKNet.h"



KKNet::KKNet()
{
	
}


KKNet::~KKNet()
{
}


void KKNet::Decode(BYTE* data, int num)
{
	BYTE keyc = data[0];
	//data[0] = (byte)(data[0] ^ keyc);
	for (int i = 0; i < num - 1; i++)
	{
		data[i + 1] = keymap[0x100 + data[i + 1]];
		if (((1 << (keyc & 7)) & keyc) != 0)
		{
			BYTE tmp = data[i + 1];
			data[i + 1] = (BYTE)(keyc ^ data[i + 1]);
			keyc = tmp;
		}
		else
		{
			if (((1 << (keyc & 3)) & keyc) != 0)
			{
				BYTE tmp = data[i + 1];
				data[i + 1] = (BYTE)(data[i + 1] - keyc);
				if (data[1 + i] < 0)
				{
					data[1 + i] = (BYTE)(data[1 + i] & 0xffffffff);
				}
				keyc = tmp;
			}
			else
			{
				BYTE tmp = data[i + 1];
				data[1 + i] = (BYTE)(data[1 + i] + keyc);
				keyc = tmp;
			}
		}
	}
}

void KKNet::Encode(BYTE* data, int num, BYTE keyc)
{
	BYTE key = keyc;
	//byte keyc = data[0];
	for (int i = 1; i < num; i++)
	{
		if (((1 << (key & 7)) & key) != 0)
		{
			data[i] = (BYTE)(key ^ data[i]);
			key = data[i];
			data[i] = keymap[data[i]];
		}
		else
		{
			if (((1 << (key & 3)) & key) != 0)
			{
				data[i] = (BYTE)(data[i] + key);
				key = data[i];
				data[i] = keymap[data[i]];
			}
			else
			{
				data[i] = (BYTE)(data[i] - key);
				if (data[i] < 0)
				{
					data[i] = (BYTE)(data[i] & 0xffffffff);
				}
				key = data[i];
				data[i] = keymap[data[i]];
			}
		}
	}
}