# Red Badger Code Test: Martian Robots

This repository contains an implementation of **Martian Robots**.

## Overview

The `Program` class pipes `Console.In` into the `Interpreter` and uses its `ProcessInput` method to execute commands. It is responsible for handling the input/output from the command line, ensuring simplicity for command line usage while maintaining separation for unit testing purposes.

### Components

* **Interpreter**: The `Interpreter` takes a `TextReader` input and processes the designated input format. It tracks lost rovers and prevents subsequent rovers in the same position and orientation from repeating actions that led to a previous rover becoming 'lost'. This keeps the focus on solving the problem without overcomplicating the model.

* **Rover**: A read-only `Rover` class that interprets the commands `L`, `R`, and `F`. The rover knows how to move but is unaware of the world around it.

### Design Decisions

* **Simplified World Representation**: I chose not to implement a dedicated Map or Grid object that allocates memory for every possible cell. This design decision avoids unnecessary memory limitations.

* **Incremental Evolution**: At this stage, the code is intentionally kept minimal and straightforward. The next steps would depend on real-world use cases:

  * **Feature Expansion**: Once in production, additional feature requests might necessitate refactoring to keep the implementation clean.
  * **Performance Considerations**: Any performance issues would also guide future changes to optimize the solution effectively.

## Running the Code

You can execute the program by running it from the command line, providing the input as described in the problem statement.

You can enter each line pressing enter at the end of the line and then use `Ctrl+D` when you are done.

Otherwise you can put the test input into a file and pass it to the program as follow.

```
dotnet run --project src/MarsRover/MarsRover.csproj < test.txt
```

# Running the tests

Run `test.sh` to run unit tests and the example from the "Developer Programming Problem".

```
./test.sh
```

