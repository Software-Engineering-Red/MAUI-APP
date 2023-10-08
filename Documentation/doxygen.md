## The Doxygen Documentation Process

### Commenting Code 

Each Team member has to provide commentary to their own written code
according to the [.NET guidelines](https://learn.microsoft.com/en-gb/dotnet/csharp/language-reference/xmldoc/).

The commentary for the Documentation is supposed to shortly summarize the purpose and functionality of every 
* class
* method
* instance variable
* interface
* enum
* (In principle all somewhat comprehensive modules in C#)

The Documentation should not include any internal Comments inside these Modules.

### Generation

The generation can be executed by first of all making sure doxygen is [installed](https://www.doxygen.nl/download.html).
After that you have to open the Doxywizzard, select the Doxyfile from
```\Documentation\doxygen_config\Doxyfile```, navigate to the Run Tab in the Wizzard and finally pressing **run doxygen**.
The Documentation will then be generated in the reference-folder and can be commited and pushed according to the workflow.

### Specifications

* Doxygen should not generate any LaTeX (```GENERATE_LATEX = NO```). 
* The selected Programming Language is Java/C#.
* During the Build it should extract private (```EXTRACT_PRIVATE = YES```), static (```EXTRACT_STATIC = YES```) members
* It should especially include our written internal documentation in the form of comments (```INTERNAL_DOCS = YES```)
* The Rest of the Specification in the Configurations are the standar 
