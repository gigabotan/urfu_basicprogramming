using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Input;
using Digger.Architecture;

namespace Digger;

public class Terrain : ICreature
{
    public CreatureCommand Act(int x, int y) => new CreatureCommand();

    public bool DeadInConflict(ICreature conflictedObject) => conflictedObject is Player;

    public int GetDrawingPriority() => 1;

    public string GetImageFileName() => "Terrain.png";
}

public class Player : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        var result = GetMovementCommand();
        var newX = x + result.DeltaX;
        var newY = y + result.DeltaY;

        if (IsOutOfBounds(newX, newY) || IsBlockedBySack(newX, newY))
        {
            result.DeltaX = 0;
            result.DeltaY = 0;
        }

        if (result.DeltaX != 0 || result.DeltaY != 0)
            result.TransformTo = this;

        return result;
    }

    private CreatureCommand GetMovementCommand()
    {
        var result = new CreatureCommand();
        switch (Game.KeyPressed)
        {
            case Key.Right: result.DeltaX = 1; break;
            case Key.Left: result.DeltaX = -1; break;
            case Key.Down: result.DeltaY = 1; break;
            case Key.Up: result.DeltaY = -1; break;
        }
        return result;
    }

    private bool IsOutOfBounds(int x, int y) =>
        x < 0 || x >= Game.MapWidth || y < 0 || y >= Game.MapHeight;

    private bool IsBlockedBySack(int x, int y) =>
        !IsOutOfBounds(x, y) && Game.Map[x, y] is Sack;

    public bool DeadInConflict(ICreature conflictedObject) =>
        conflictedObject is Monster ||
        (conflictedObject is Sack sack && sack.FallCounter > 1);

    public int GetDrawingPriority() => 0;

    public string GetImageFileName() => "Digger.png";
}

public class Sack : ICreature
{
    public int FallCounter { get; set; }

    public CreatureCommand Act(int x, int y)
    {
        var result = new CreatureCommand();
        int belowY = y + 1;

        if (CanFall(x, belowY))
        {
            result.DeltaY = 1;
            FallCounter++;
            result.TransformTo = this;
        }
        else if (FallCounter > 0)
        {
            result.TransformTo = FallCounter > 1 ? new Gold() : this;
            FallCounter = 0;
        }

        return result;
    }

    private bool CanFall(int x, int belowY) =>
        belowY < Game.MapHeight &&
        (Game.Map[x, belowY] == null ||
         (FallCounter > 0 && Game.Map[x, belowY] is Player) ||
         (FallCounter > 0 && Game.Map[x, belowY] is Monster));

    public bool DeadInConflict(ICreature conflictedObject) => false;

    public int GetDrawingPriority() => 5;

    public string GetImageFileName() => "Sack.png";
}

public class Gold : ICreature
{
    public CreatureCommand Act(int x, int y) => new CreatureCommand();

    public bool DeadInConflict(ICreature conflictedObject)
    {
        if (conflictedObject is Player)
        {
            Game.Scores += 10;
            return true;
        }
        if (conflictedObject is Monster)
            return true;
        return false;
    }

    public int GetDrawingPriority() => 3;

    public string GetImageFileName() => "Gold.png";
}

public class Monster : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        var playerPosition = FindPlayer();
        if (playerPosition == null)
            return new CreatureCommand();

        var nextStep = FindNextStepToPlayer(x, y, playerPosition.Value);
        if (nextStep == null)
            return new CreatureCommand();

        var (nextX, nextY) = nextStep.Value;
        return new CreatureCommand
        {
            DeltaX = nextX - x,
            DeltaY = nextY - y,
            TransformTo = this
        };
    }

    private (int x, int y)? FindPlayer()
    {
        for (int x = 0; x < Game.MapWidth; x++)
            for (int y = 0; y < Game.MapHeight; y++)
                if (Game.Map[x, y] is Player)
                    return (x, y);
        return null;
    }

    private (int x, int y)? FindNextStepToPlayer(int startX, int startY, (int x, int y) playerPos)
    {
        if (startX == playerPos.x && startY == playerPos.y)
            return null;

        var queue = new Queue<(int x, int y)>();
        var visited = new HashSet<(int x, int y)>();
        var parent = new Dictionary<(int x, int y), (int x, int y)?>();

        queue.Enqueue((startX, startY));
        visited.Add((startX, startY));
        parent[(startX, startY)] = null;

        var directions = new[] { (0, 1), (0, -1), (1, 0), (-1, 0) };

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.x == playerPos.x && current.y == playerPos.y)
                return ReconstructFirstStep(parent, current, startX, startY);

            foreach (var (dx, dy) in directions)
            {
                var newX = current.x + dx;
                var newY = current.y + dy;
                var newPos = (newX, newY);

                if (!visited.Contains(newPos) && CanMove(newX, newY))
                {
                    visited.Add(newPos);
                    parent[newPos] = current;
                    queue.Enqueue(newPos);
                }
            }
        }

        return null;
    }

    private (int x, int y)? ReconstructFirstStep(
        Dictionary<(int x, int y), (int x, int y)?> parent,
        (int x, int y) target,
        int startX,
        int startY)
    {
        var path = new List<(int x, int y)>();
        var current = target;

        while (parent[current] != null)
        {
            path.Add(current);
            current = parent[current].Value;
        }

        path.Reverse();
        return path.Count > 0 ? path[0] : ((int x, int y)?)null;
    }

    private bool CanMove(int x, int y)
    {
        if (x < 0 || x >= Game.MapWidth || y < 0 || y >= Game.MapHeight)
            return false;

        var creature = Game.Map[x, y];
        return creature == null || creature is Player || creature is Gold;
    }

    public bool DeadInConflict(ICreature conflictedObject) =>
        conflictedObject is Monster ||
        (conflictedObject is Sack sack && sack.FallCounter > 1);

    public int GetDrawingPriority() => 2;

    public string GetImageFileName() => "Monster.png";
}
