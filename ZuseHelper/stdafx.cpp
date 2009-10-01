/*
 * Zuse - A Zune Last.fm plugin
 * Copyright (C) 2007-2009 Zachary Howe
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#include "stdafx.h"

void InitParams(LPTSTR lpCmdLine)
{
	for (int loop = 0; loop < lstrlen(lpCmdLine); loop++)
	{
		switch (lpCmdLine[loop])
		{
			case '-':
			{
				WCHAR* wch_option = &lpCmdLine[loop + 1];
				char* ch_option = (char*)wch_option;

				int cmp = strcmp(ch_option, "-start");

				switch (cmp)
				{
					case 0:
						MessageBox(NULL, L"INFO: Start params match", L"ZuseHelper", 0);
						break;
					default:
						int const arraysize = 30;
						TCHAR pszDest[arraysize];
						size_t cbDest = arraysize * sizeof(TCHAR);
						LPCTSTR pszFormat = TEXT("INFO: Start params: %d");

						HRESULT hr = StringCbPrintf(pszDest, cbDest, pszFormat, cmp);

						MessageBox(NULL, pszDest, L"ZuseHelper", 0);
						break;
				}
			}
		}
	}
}