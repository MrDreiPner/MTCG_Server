Database Setup:
Create DB:
- Name: MTCG_DB
- Port: 10002
- User: postgres
- PW  : Aurora

Create type -> element_id
Command is commented out in Database.cs and only needs to be run the first time after the db is created.
Run BEFORE any CREATE TABLE commands
"CREATE TYPE element_id AS ENUM ('water', 'fire', 'normal');"

Curl Script:
Added 2 calls:
- Starting a battle without having a configured Deck
- Trying to configure a Deck with a card not owned by the user

Unique Feature: Scaling Elo calculations
Elo calculations are dependent on the difference between players.
The lower elo player has a capped Elo loss of 10 points, while the higher elo player only gains half the possible points.
If the lower elo player wins, he gains all the points calculated (the higher the gap, the more points are gained. capped at 50 points)
and the losing higher elo player loses the equivalent amount of points.
A player can not fall below 0 elo.
Calculation: 
Difference = (|elo1 - elo2|)/100 
BasePoints = 10
Calculated Points = BasePoints * Difference



