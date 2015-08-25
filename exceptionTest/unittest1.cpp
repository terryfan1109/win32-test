#include "stdafx.h"

#include "CppUnitTest.h"

#include <exception>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

/////////////////////////////////////////////////////////////////////////////
//
class MyException: public std::exception {
  Logger logger;

public:
  MyException() {
    logger.WriteMessage(L"MyException constructor");
  }
  MyException(const MyException&) {
    logger.WriteMessage(L"MyException copy constructor");
  }
  ~MyException() {
    logger.WriteMessage(L"MyException destructor");
  }
};

/////////////////////////////////////////////////////////////////////////////
//
class MyClass {
  static Logger logger;

public:
  static void fun_1() {
    std::exception_ptr ex;

    try {
      fun_2();
    }
    catch (std::exception& e) {
      logger.WriteMessage(ToString((unsigned long)&e).c_str());
      ex = std::current_exception();
      logger.WriteMessage(ToString((unsigned long)&ex).c_str());
    }
  }

  static void fun_2() {
    try {
      fun_3();
    }
    catch (MyException& e) {
      logger.WriteMessage(ToString((unsigned long) &e).c_str());
      throw;
    }
  }

  static void fun_3() {
    try {
      fun_4();
    }
    catch (MyException& e) {
      logger.WriteMessage(ToString((unsigned long) &e).c_str());
      throw;
    }
  }

  static void fun_4() {
    throw MyException();
  }
};

Logger MyClass::logger;


namespace exceptionTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(testException1)
		{
      MyClass::fun_1();
		}
	};
}