using GameEngine.Data;
using GameEngine.Data.Enums;
using GameEngine.Data.Factorys;
using GameEngine.Data.Interfaces;
using GameEngine.Data.Models;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Debug
{
    public class Debug : GameEngine.Data.System, IDebug
    {
        private Text TxtFps;

        private int FrameCounter = 0;
        private double FrameTime = 0;
        private int Fps = 0;

        private Clock Time;
        private Time LastFrame;

        public Debug(GameEngine.Data.System system)
            : base(system)
        {

        }

        public override void Start()
        {
            IsRunning = true;

            Time = new Clock();

            TxtFps = Parent.Graphic.AddText(EnumLayer.Ui, 10, 10, 30, Color.Green);

        }

        public void Data()
        {

            //Parent.Graphic.AddRectangle(EnumLayer.Background, 100, 100, 100, 100, Color.Red);

            //Parent.Graphic.AddRectangle(EnumLayer.Principal, 150, 150, 100, 100, Color.Yellow);

            //Parent.Graphic.AddRectangle(EnumLayer.Foreground, 200, 200, 100, 100, Color.Green);

            //Parent.Graphic.AddRectangle(EnumLayer.Foreground, 300, 300, 100, 100, Color.Cyan);

            //Parent.Graphic.AddRectangle(EnumLayer.Ui, 250, 250, 100, 100, Color.Red);

            Parent.Graphic.AddSprite(EnumLayer.Principal, 700, 500, Color.White, "character.png");

            Parent.Graphic.AddSpriteAnimated(EnumLayer.Principal, "character.png", 57, 64, 10, 500, 200, Color.Red, 40, 47, true, true, 
                57, 64);

            Parent.Graphic.AddSpriteAnimated(EnumLayer.Principal, "character.png", 57, 64, 10, 550, 200, Color.Cyan, 48, 55, true, true,
                57, 64);

            Parent.Graphic.AddSpriteAnimated(EnumLayer.Principal, "character.png", 57, 64, 10, 600, 200, Color.Yellow, 56, 63, true, true,
                57, 64);

            var sprite = SpriteFactory.Instance.GetSpriteAnimated("character.png", 57, 64, 10, 40, 47, true, true,
                57, 64);
            sprite.Color = Color.Green;

            var state = new RenderStates()
            {
                BlendMode = BlendMode.Alpha,
                Transform = Transform.Identity
            };

            state.Transform.Translate(500, 100);

            var spriteObject = new SpriteObject(sprite, state, this.HoverStart, this.HoverEnd, this.Click);

            Parent.Graphic.AddSpriteObject(EnumLayer.Principal, spriteObject);

            //====================

            sprite = SpriteFactory.Instance.GetSpriteAnimated("button.png", 186, 52, 0, 0, 2, false, false,
                186, 52);
            sprite.Color = Color.Green;

            state = new RenderStates()
            {
                BlendMode = BlendMode.Alpha,
                Transform = Transform.Identity
            };

            state.Transform.Translate(200, 100);

            spriteObject = new SpriteObject(sprite, state, this.HoverStart2, this.HoverEnd2, this.Click2);

            spriteObject.Sprite.SetAnimation(1, 1, false, false);


            Parent.Graphic.AddSpriteObject(EnumLayer.Principal, spriteObject);

            
        }

        public void Click(SpriteObject obj)
        {
            //Console.WriteLine("Click");
            obj.Sprite.Color = Color.Red;
        }

        public void HoverStart(SpriteObject obj)
        {
            //Console.WriteLine("HoverStart");
            obj.Sprite.Color = Color.Yellow;
        }

        public void HoverEnd(SpriteObject obj)
        {
            //Console.WriteLine("HoverEnd");
            obj.Sprite.Color = Color.Green;
        }

        public void Click2(SpriteObject obj)
        {
            //obj.Sprite.SetFrame(2);
            obj.Sprite.SetAnimation(2, 2, false, false);
        }

        public void HoverStart2(SpriteObject obj)
        {
            //obj.Sprite.SetFrame(1);
            obj.Sprite.SetAnimation(0, 0, false, false);
        }

        public void HoverEnd2(SpriteObject obj)
        {
            //obj.Sprite.SetFrame(0);
            obj.Sprite.SetAnimation(1, 1, false, false);
        }

        public override void Update(double elapsedTime = 0.0)
        {
            FrameCounter++;
            FrameTime += (Time.ElapsedTime - LastFrame).AsMicroseconds();

            if (FrameTime >= 1000000)
            {
                Fps = FrameCounter;
                FrameCounter = 0;
                FrameTime -= 1000000;
            }

            LastFrame = Time.ElapsedTime;

            TxtFps.DisplayedString = String.Format("{0} ({1} ms)", Fps.ToString("0.0"), elapsedTime.ToString("0.0"));
        }

        public override void Stop()
        {
            IsRunning = false;
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
