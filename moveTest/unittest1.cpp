#include "stdafx.h"

#include "CppUnitTest.h"

#include <strstream>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

class MyObject {
  static Logger logger;
  std::wstring value;

public:
  MyObject() {
    logger.WriteMessage(L"cotr");    
  }

  MyObject(std::wstring value) {
    logger.WriteMessage(L"cotr");    
    this->value = value;
  }

  MyObject(const MyObject& rhs) {
    logger.WriteMessage(L"copy cotr");
    value = rhs.value;
  }

  MyObject(MyObject&& rhs) {
    logger.WriteMessage(L"move cotr");
    value = std::move(rhs.value);
  }

  MyObject& operator=(const MyObject& rhs) {
    logger.WriteMessage(L"assigment op");
    value = rhs.value;
  }

  MyObject& operator=(MyObject&& rhs) {
    logger.WriteMessage(L"move assignment op");
    value = std::move(rhs.value);
  }

  ~MyObject() {
    logger.WriteMessage(L"dstr");
  }

  std::wstring toString() {
    auto& buf = std::wstringstream();
    buf << L"{\'value\': \'" << value << L"\'}";
    return std::wstring(std::move(buf.str()));
  }
};

Logger MyObject::logger;

MyObject fa() {
  MyObject a;
  return a;
}

namespace moveTest
{		
	TEST_CLASS(UnitTest1)
	{
    static Logger logger;

  public:
		
		TEST_METHOD(moveTest1)
		{
      logger.WriteMessage(L"moveTest1\r\n>>>");
      MyObject a;
      MyObject b = a;
      logger.WriteMessage(L"<<<");
		}

    TEST_METHOD(moveTest2)
    {
      logger.WriteMessage(L"moveTest2\r\n>>>");
      fa();
      logger.WriteMessage(L"<<<");
    }

    TEST_METHOD(moveTest3)
    {
      logger.WriteMessage(L"moveTest3\r\n>>>");
      MyObject b = fa();
      logger.WriteMessage(L"<<<");
    }

    TEST_METHOD(moveTest4)
    {
      logger.WriteMessage(L"moveTest4\r\n>>>");
      MyObject b = std::move(fa());
      logger.WriteMessage(L"<<<");
    }

    TEST_METHOD(moveTest5)
    {
      logger.WriteMessage(L"moveTest5\r\n>>>");
      MyObject a(std::wstring(L"x"));
      MyObject b = std::move(a);
      logger.WriteMessage(a.toString().c_str());
      logger.WriteMessage(L"<<<");
    }

    TEST_METHOD(moveTest6)
    {
      logger.WriteMessage(L"moveTest5\r\n>>>");
      MyObject a(std::wstring(L"x"));
      MyObject b = a;
      logger.WriteMessage(a.toString().c_str());
      logger.WriteMessage(L"<<<");
    }

	};

  Logger UnitTest1::logger;
}