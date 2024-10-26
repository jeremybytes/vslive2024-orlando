# Take Your C# Skills to the Next Level   
**Full-day Workshop**  
*Level: Intermediate*  

## Description
Are you a C# developer eager to create software that is easy to maintain, has minimal bugs, and makes your users happy? If so, this workshop is for you.  

Through lectures, code samples, and hands-on labs, you will learn how interfaces, delegates, dependency injection, and unit testing can help you write applications that are easy to extend, change, and debug. Along the way you will also encounter modern C# topics such as nullability, switch expressions, and asynchronous code. In the end, you will take away practical experience that you can immediately apply to your own projects. Topics will include:  

* **Interfaces**: Explore the art of writing clean, modular code through interfaces. Learn how interfaces facilitate code extension, make your applications more testable, and lead to fewer bugs.  
* **Delegates**: Gain a deep understanding of delegates and learn how to use them to build flexible and extensible code.  
* **Dependency Injection**: Discover the magic of dependency injection and how it promotes loose coupling, making your codebase more flexible, testable, and maintainable.  
* **Unit Testing**: Write tests that ensure your code functions as expected, enabling you to catch bugs early in the development process.  

Key Benefits:  

* **Easier Maintenance**: Explore practices for writing clean, maintainable code that’s a breeze to work with, whether you’re collaborating with a team or revisiting your own projects down the road.  
* **Fewer Bugs**: Learn the art of writing robust code that is easy to test. Find bugs before your users do, and be confident that code changes do not break existing functionality.  
* **Happier Users**: The point of writing software is to meet your user’s needs. With these tools in hand, you can create applications that not only meet but exceed user expectations.  

Pre-Requisites:  

Basic understanding of C# and object-oriented programming (classes, inheritance, methods, and properties). No prior experience with the primary topics is necessary; we’ll take care of that as we go.  

## Hands-on Lab Requirements:

* You must provide your own laptop computer for the hands-on lab portions of the day.

* Interactive labs can be completed using Windows, macOS, or Linux -- anywhere .NET 8 will run.

* You need to have the .NET 8 SDK installed as well as the code editor of your choice (Visual Studio 2022 Community Edition or Visual Studio Code are both good (free) choices).  

* You will also need the lab files with the code and instructions. These can be obtained from this repository (in the Labs folder) or from the separate "Labs Only" repository noted below.

### Links:

* .NET 8.0 SDK  
[https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download)

* Visual Studio 2022 (Community)  
[https://visualstudio.microsoft.com/downloads/](https://visualstudio.microsoft.com/downloads/)  
Note: Install the "ASP.NET and web development" workload for the labs and samples. Include ".NET desktop development" for "digit-display" sample and WPF-based samples.

* Visual Studio Code  
[https://code.visualstudio.com/download](https://code.visualstudio.com/download)  

* Labs Only repository  
[https://github.com/jeremybytes/vslive2024-orlando-workshop-labs](https://github.com/jeremybytes/vslive2024-orlando-workshop-labs)  
A repository with just the lab files for this workshop, so you do not need to download this entire repository.  


## Hands-on Labs

The "Labs" folder contains the hands-on labs. As noted above, this workshop will not have dedicated lab time, but each lab has a step-by-step walkthrough if you want to try things out yourself.  

* [Lab 00 - Application Overview](Labs/Lab00/)  
* [Lab 01 - Adding Interfaces for Better Control](Labs/Lab01/)  
* [Lab 02 - Adding Unit Tests](Labs/Lab02/)  
* [Lab 03 - More Dependency Injection](Labs/Lab03/)  

Each lab consists of the following:  
* **Labxx-Instructions** (Markdown)  
A markdown file containing the lab instructions. This includes a set of goals and step-by-step instructions. This can be viewed on GitHub or in Visual Studio Code (just click the "Open Preview to the Side" button in the upper right corner).  

* **dotnet8** (Folder)  
Lab code is available for .NET 8.0.

    * **Starter** (Folder)  
    This folder contains the starting code for the lab.  
    * **Completed** (Folder)  
    This folder contains the completed solution. If at any time you get stuck during the lab, you can check this folder for a solution.  

In addition, each lab builds on the previous. So the "Completed" of Lab 01 is the "Starter" of Lab 02. If you go through the labs in order, you can keep using the same solution as you move from lab to lab.

## Resources  

**Slides and Code Samples**  
* [Workshop Slides](./SLIDES-csharp-workshop.pdf)  
* [Code Samples](./DemoApps/)

**Abstraction / Implementation**  
* Article: [Drawbacks to Abstraction](http://jeremybytes.blogspot.com/2012/11/drawbacks-to-abstraction.html)  
* Article: [Abstraction: The Goldilocks Principle](http://jeremybytes.blogspot.com/2012/10/abstraction-goldilocks-principle.html)  
* Article: [Explicit Interface Implementation](http://jeremybytes.blogspot.com/2012/03/explicit-interface-implementation.html)  
* Article: [Updating an Interface Implementation](http://jeremybytes.blogspot.com/2012/03/updating-interface-implementation.html)  

**Dynamic Loading**    
* Article: [Dyamically Loading .NET Standard Assemblies in .NET Core](https://jeremybytes.blogspot.com/2020/01/using-typegettype-with-net-core.html)  
* Article: [Dynamically Loading Types in .NET Core with a Custom Assembly Load Context](https://jeremybytes.blogspot.com/2020/01/dynamically-loading-types-in-net-core.html)  

**Framework / Language Changes**  
* Topic: [Catching Up with Interfaces: What You Know May Be Wrong](http://www.jeremybytes.com/Demos.aspx#CSharp8Interfaces)  
* Article: [A Closer Look at C# 8 Interfaces](https://jeremybytes.blogspot.com/2019/09/a-closer-look-at-c-8-interfaces.html)  
* Article: [Properties and Default Implementation](https://jeremybytes.blogspot.com/2019/09/c-8-interfaces-properties-and-default.html)  
* Article: [Dangerous Assumptions in Default Implementation](https://jeremybytes.blogspot.com/2019/09/c-8-interfaces-dangerous-assumptions-in.html)  
* Article: ["dynamic" and Default Implementation](https://jeremybytes.blogspot.com/2019/09/c-8-interfaces-dynamic-and-default.html)  
* Article: [Unit Testing Default Implementation](https://jeremybytes.blogspot.com/2019/09/c-8-interfaces-unit-testing-default.html)  
* Article: [Public, Private, and Protected Members](https://jeremybytes.blogspot.com/2019/11/c-8-interfaces-public-private-and.html)  
* Article: [Static Members](https://jeremybytes.blogspot.com/2019/12/c-8-interfaces-static-members.html)  
* Article: [Static Main -- Why Not?](https://jeremybytes.blogspot.com/2019/12/c-8-interfaces-static-main-why-not.html)  

**Dependency Injection Patterns**  
* [Dependency Injection: The Property Injection Pattern](http://jeremybytes.blogspot.com/2014/01/dependency-injection-property-injection.html)  
* [Property Injection: Simple vs. Safe](http://jeremybytes.blogspot.com/2015/06/property-injection-simple-vs-safe.html)  
* [Dependency Injection: The Service Locator Pattern](http://jeremybytes.blogspot.com/2013/04/dependency-injection-service-locator.html)  

**Decorators and Async Interfaces**
* [Async Interfaces, Decorators, and .NET Standard](https://jeremybytes.blogspot.com/2019/01/more-di-async-interfaces-decorators-and.html)  
* [Async Interfaces](https://jeremybytes.blogspot.com/2019/01/more-di-async-interfaces.html)  
* [Adding Retry with the Decorator Pattern](https://jeremybytes.blogspot.com/2019/01/more-di-adding-retry-with-decorator.html)  
* [Unit Testing Async Methods](https://jeremybytes.blogspot.com/2019/01/more-di-unit-testing-async-methods.html)  
* [Adding Exception Logging with the Decorator Pattern](https://jeremybytes.blogspot.com/2019/01/more-di-adding-exception-logging-with.html)  
* [Adding a Client-Side Cache with the Decorator Pattern](https://jeremybytes.blogspot.com/2019/01/more-di-adding-client-side-cache-with.html)  
* [The Real Power of Decorators -- Stacking Functionality](https://jeremybytes.blogspot.com/2019/01/more-di-real-power-of-decorators.html)  

**Working with Statics (DateTime.Now)**  
* [Mocking Current Time with a Simple Time Provider](https://jeremybytes.blogspot.com/2015/01/mocking-current-time-with-time-provider.html)  

**Delegates**
* [C# Delegates](http://www.jeremybytes.com/Downloads/GetFuncyWithDelegates.pdf) - PDF Walkthrough of the code samples.  
Note: This is a bit old, but the code is all still valid.
* [Exceptions in Multicast Delegates](http://jeremybytes.blogspot.com/2011/11/exceptions-in-multi-cast-delegates.html)  
* [More Delegate Exception Handling](http://jeremybytes.blogspot.com/2013/03/more-delegate-exception-handling.html)  
* [Recognizing Higher-Order Functions in C#](http://jeremybytes.blogspot.com/2014/06/recognizing-higher-order-function-in-c.html)  
* [Named Delegates Compared to Anonymous Delegates](http://jeremybytes.blogspot.com/2015/03/named-delegates-compared-to-anonymous.html)  

**Unit Testing**
* Book Review (Highly Recommended): [The Art of Unit Testing - Roy Osherove](http://jeremybytes.blogspot.com/2015/06/book-review-art-of-unit-testing-with.html)  
* Book Review: [Working Effectively with Legacy Code - Michael C. Feathers](http://jeremybytes.blogspot.com/2013/02/book-review-working-effectively-with.html)  
* Book Review: [Test-Driven Development by Example - Kent Beck](http://jeremybytes.blogspot.com/2013/03/book-review-test-driven-development-by.html)  
* Article Series (TDD & NUnit): [Coding Practice with Conway's Game of Life](http://www.jeremybytes.com/Downloads.aspx#ConwayTDD)  
* Video Series (Intro to TDD): [Unit Testing & TDD](https://www.youtube.com/watch?v=l4xhTq4qmC0&list=PLdbkZkVDyKZXqPu-xDFkzuP66QijGeewz)  

**Testing Practices**  
* Code Coverage: [What Parts of Your Application Do Your Users *Not* Care About?](http://jeremybytes.blogspot.com/2015/02/unit-test-coverage-what-parts-of-your.html)  
* What to Test: [My Approach to Testing: Test Public Members](http://jeremybytes.blogspot.com/2015/04/my-approach-to-testing-test-public.html)  
* Setup Methods: [Unit Testing: Setup Methods or Not?](http://jeremybytes.blogspot.com/2015/06/unit-testing-setup-methods-or-not.html)  
* Assert Messages: [Unit Testing Asserts: Skip the Message Parameter](http://jeremybytes.blogspot.com/2015/07/unit-testing-asserts-skip-message.html)  

---