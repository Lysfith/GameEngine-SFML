using GameEngine.Data.Enums;
using GameEngine.Data.Model;
using GameEngine.Data.Models;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Data.Interfaces
{
    public interface IGraphic : ISystem
    {
        //RectangleShape AddRectangle(EnumLayer layer, int x, int y, int width, int height, Color color);
        Sprite AddSprite(EnumLayer layer, int x, int y, Color color, string textureName, int width = 0, int height = 0, bool repeated = false);
        SpriteAnimated AddSpriteAnimated(EnumLayer layer, string textureName, int frameWidth, int frameHeight, int framesPerSecond, int x, int y, Color color,
           int firstFrame = 0, int lastFrame = 0, bool isAnimated = false, bool isLooped = true, int width = 0, int height = 0);
        Text AddText(EnumLayer layer, int x, int y, uint fontSize, Color color);
        SpriteObject AddSpriteObject(EnumLayer layer, SpriteObject sprite);
        void ClearLayer(EnumLayer layer);
        List<SpriteObject> RayCast(EnumLayer layer, int x, int y);
        RenderWindow GetWindow();
        int GetWidth();
        int GetHeight();
    }
}
