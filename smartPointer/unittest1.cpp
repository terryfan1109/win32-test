#include "stdafx.h"

#include <memory>
#include <string>

#include <Windows.h>
#include "CppUnitTest.h"

using std::shared_ptr;
using std::wstring;

using namespace Microsoft::VisualStudio::CppUnitTestFramework;


class MyObject {
public:
	typedef shared_ptr<const wstring> wstringPtr;

	static const wstringPtr EMPTY_STRING_PTR;
 
public:
	MyObject() {
		name = EMPTY_STRING_PTR;
		description = EMPTY_STRING_PTR;
	}

	void setName(wstringPtr value) {
		name = value;
	}
	wstringPtr getName() const {
		return name;
	}

	void setDescription(wstringPtr value) {
		description = value;
	}

	wstringPtr getDescription() const {
		return description;
	}

private:
	wstringPtr name;
	wstringPtr description;
};

const MyObject::wstringPtr MyObject::EMPTY_STRING_PTR(new wstring);

namespace smartPointer
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(testSmartPointer1)
		{
			MyObject a;
			a.setName(MyObject::wstringPtr(new wstring(L"abc")));
			a.setDescription(MyObject::wstringPtr(new wstring(L"description")));

			wchar_t buffer[MAX_PATH] = {0};
			swprintf_s(buffer, L"%s(%s)\r\n", a.getName()->c_str(), a.getDescription()->c_str());
			OutputDebugString(buffer);
		}

	};
}