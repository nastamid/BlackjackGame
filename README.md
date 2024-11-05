🃏 Blackjack Console Game

Welcome to the Blackjack Console Game! This is a classic Blackjack game built purely in C# for both single-player and multiplayer modes. 
The goal is simple: beat the dealer by getting as close as possible to 21 without busting. 
This project showcases scalable, maintainable code design using advanced development practices.

🎮 Features
Single-Player Mode: Play against a table of bots, each making very simple decisions (if they have more than 17 they hold).
Multiplayer Mode: Compete with friends to see who can come closest to 21!
Scalable Design: Easily expandable architecture, allowing for new player types and game features.
Customizable Outcomes: The game evaluator handles win, loss, and draw logic in a modular way.
Test-Driven Development (TDD): Developed using TDD principles for robust, stable code and safe adjustments.

📸 Demo

![Blackjack](https://github.com/user-attachments/assets/a1e15e95-16fd-4d8b-9635-49459cc4f2c5)

🚀 Getting Started
Clone this repository:

bash
Copy code
git clone [https://github.com/yourusername/blackjack-console-game.git](https://github.com/nastamid/BlackjackGame.git)
cd BlackjackGame
Build and run the project

Or just find an exe file:
bash
Copy code
 BlackJack\bin\Release\BlackJack.exe
Enjoy the game! 🎉

📖 Gameplay Rules
The game follows standard Blackjack rules:

Objective: Get closer to 21 than the dealer without going over.
Dealer Rules: Dealer stands on 17 or higher.
Winning Conditions: Beat the dealer's hand or have the highest hand if the dealer busts.

🛠️ Project Structure and Design
This project demonstrates how to build a maintainable, scalable codebase from the ground up.

🧩 Strategy Pattern for Player Types
The Player class uses the Strategy Pattern to handle different player types: Human, Bot, and Dealer.
New player types, such as AI or Multiplayer, can be easily added with minimal changes.

🔍 Game Evaluator for Outcome Management
The GameEvaluator class manages win/loss logic.
Each outcome condition is encapsulated in a separate class, following the Single Responsibility Principle.
This design makes logic modular and easy to test or expand.

🧪 Test-Driven Development (TDD)
Core logic was built with TDD. Tests were created before implementation to ensure that adjustments wouldn’t break the core functionality.
Edge cases like multiple players busting, draws, and complex win conditions are thoroughly tested.

🛠️ Technologies Used
Language: Built purely in C# to leverage object-oriented principles for a clean, modular codebase.

🚀 Future Enhancements
The design is highly scalable, making future improvements simple to implement:

Advanced Bot Strategies: Create more sophisticated bot behaviors to simulate real players.
Real-Time Multiplayer: Expand the multiplayer feature for real-time gameplay.
Graphical User Interface: Upgrade from console-based to a graphical interface for a more engaging experience.

🤝 Contributing
Contributions are welcome! Feel free to fork this repository, make your changes, and submit a pull request.

📄 License
This project is licensed under the MIT License. See the LICENSE file for details.

Feel free to add or adjust the links to images and the installation instructions based on your specific setup. Let me know if you need more visuals or help with any of the sections!
