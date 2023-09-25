# oneERNI-global-hackaton divertiteam

## Description

This repository contains the code for the oneERNI global hackathon project.

## Technologies Used for Web Application

- Programming Language: C#
- Frontend Framework: Angular 15
- Backend Framework: .NET 7

## Technologies Used for Robot side

- Programming Language: Python

## Getting Started

### Prerequisites

- Ensure you have [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0) installed on your machine.
- Ensure you have [Node.js](https://nodejs.org/) and [Angular CLI](https://angular.io/cli) installed on your machine.

### Installation

1. Clone the repository
   ```sh
   git clone https://github.com/SamuelHdez/oneERNI-global-hackaton.git
   ```
2. Navigate to the project folder
   ```sh
   cd oneERNI-global-hackaton
   ```
3. Install NPM packages
   ```sh
   npm install
   ```
4. Restore NuGet packages

5. Run the project

### Testing

- Angular Testing with Karma/Jasmine: 47 tests.

- .NET Testing with XUnit: 19 tests.

## Documentation

### Architecture
![alt text](https://github.com/SamuelHdez/oneERNI-global-hackaton-divertiteam/blob/main/Diagrams/Arch.png?raw=true)

### How to manage the robot
You can control the robot and camera by pressing the keys on your keyboard or by clicking on the keys on the interface.

ROBOT
W: Move forward
S: Move backward
A: Move to the left
D: Move to the right

CAMERA
↑: Look up
↓: Look down
←: Look to the left
→: Look to the right
SPACE: Center the camera

### Recording controls
You can save manually made routes and then send the order to the robot to repeat it automatically. To do this, you can use the buttons under the camera.

- ⏺️ Record: Recording begins. Once the recording is running you can move the robot. The system will save all movements from that point on.
- ⏸️ Stop: Stop recording. From this point on, the movements made by the robot will not be stored in the system.
- ▶️ Play: Plays back the last recording made of the robot's movements. The robot will move autonomously from the point where it is, emulating the movements stored in the system.

## Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

Distributed under the MIT License. See `LICENSE` for more information.

## Contact

- Samuel Hdez - [@SamuelHdez](https://github.com/SamuelHdez)

- Carmen Avram - [@carmenavram](https://github.com/carmenavram)

- Andrés Vázquez - [@AndresVazqz](https://github.com/AndresVazqz)

- Ferran Balaguer

- David Soto


Project Link: [https://github.com/SamuelHdez/oneERNI-global-hackaton](https://github.com/SamuelHdez/oneERNI-global-hackaton)
