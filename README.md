# TutoTOONS Task
This GitHub repository contains the completed task's project files in **TutoToonsAtranka** folder and the main game build in a **Build** folder.

The task was to make a connect the dots puzzle game with the provided data and assets.

Before starting, the task was divided into several steps:
- Read the given data from a JSON file;
- Make sure to load data to their appropriate level scenes (using Debug.Log before actually instantiating them);
- Parse the data from string array to Vector2 array;
- Convert points positions to Unity coordinate space;
- Make sure the game screen fits all screen resolutions;
- Make a point prefab with the given texture and also prepare rope material;
- Instantiate points with their index numbers;
- Make points clickable (also change their texture) and make sure that they are clicked in the correct sequence;
- Draw a line using Line Renderer between each correct point click;
- Animate the Line Renderer and make sure that the new animation starts only when the previous animation is done;
- Make the last and the first points connect automatically after each point was connected;
- Make a text fade-out animation when a point is clicked;
- Create sprites for the main menu buttons.

Below you can find concise descriptions of each script.
## Script descriptions
### FileHandler.cs
This script is used for reading and loading the given JSON file. The JSON file should be stored in the [persistent data directory](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html) and if the file does not exist when loading the game for the first time, it creates it automatically with the same given level data.
### DataHandler.cs
This script is used for parsing given data from string values to Vector2 values and also instantiating each point to its appropriate level after converting their positions to Unity coordinate space. This script also uses the Singleton pattern which lets other classes to use its variables and methods through a static instance variable. 
### LineHandler.cs
Line Handler script takes care of the main gameplay logic. It makes sure the points are clickable and changes their textures, points are clicked in the right sequence and animates the line between them, and fades out point's index text when it is clicked.
Since it was the first time working with the Line Renderer [this tutorial](https://www.youtube.com/watch?v=RMM3BAick4I&ab_channel=HamzaHerbou) was really helpful in explaining the logic behind line animation.
### MainMenuHandler.cs
This script handles the main menu buttons. Each button is stored in an array and they are assigned their appropriate scenes using a for loop.
### CameraProjection.cs
This script makes sure that the background image stays in the frame in all resolutions. Code was used from this informative [blog post](https://awesometuts.com/blog/support-mobile-screen-sizes-unity/).

*Each method is explained more in depth inside the scripts.*
## Things I learned doing this task
First of all, it was my first time doing a task for a company for a job application so I hope I managed to showcase my programming knowledge by refactoring code, using as less of iterative structures as possible, using design patterns (in this case Singleton pattern) and etc.

Also, it was my first time using Line Renderer component so it was very useful to research its functionality and how it can be applied in other situations (e.g. grappling hook). I will definitely consider using it in my future projects.

Lastly, and most importantly, I got a deeper knowledge of coordinate spaces and how to scale game objects or game screen in general to various resolutions because before it was quite a difficult subject to understand and execute correctly.
