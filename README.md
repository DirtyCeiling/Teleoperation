Unity Test API

Unity Build Status Copyright (c) 2007 - 2017 Unity Project by Mike Karlesky, Mark VanderVoord, and Greg Williams

Running Tests

RUN_TEST(func, linenum)
Each Test is run within the macro RUN_TEST. This macro performs necessary setup before the test is called and handles cleanup and result tabulation afterwards.

Ignoring Tests
