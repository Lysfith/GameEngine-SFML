using GameEngine.Data.Factorys;
using GameEngine.Data.Models;
using GameEngine.Data.Enums;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Game.GameStates.States
{
    public class MainMenuState : State
    {
        private Sprite _spriteBackground;
        private Sprite _spriteMidground;
        private Sprite _spriteForeground;
        private Random _random;

        public MainMenuState(GameEngine.Data.System system)
            : base(system)
        {
            _random = new Random();
        }

        public override void Init()
        {
            CreateBackground();
            CreateMenu();
        }


        public override void Pause()
        {

        }

        public override void Resume()
        {

        }

        public override void Key()
        {

        }

        public override void Update(double elapsedTime)
        {
            int x = _spriteBackground.TextureRect.Left + (int)(Math.Atan(_spriteBackground.TextureRect.Top));
            int x2 = _spriteForeground.TextureRect.Left - (int)(Math.Atan(_spriteForeground.TextureRect.Top));

            _spriteBackground.TextureRect = new IntRect(x, _spriteBackground.TextureRect.Top - 1,
                _spriteBackground.TextureRect.Width, _spriteBackground.TextureRect.Height);
            _spriteMidground.TextureRect = new IntRect(_spriteMidground.TextureRect.Left, _spriteMidground.TextureRect.Top - 2,
                _spriteMidground.TextureRect.Width, _spriteMidground.TextureRect.Height);
            _spriteForeground.TextureRect = new IntRect(x2, _spriteForeground.TextureRect.Top - 3,
                _spriteForeground.TextureRect.Width, _spriteForeground.TextureRect.Height);
            
        }

        public override void Render()
        {

        }

        public override void Dispose()
        {

        }

        private void CreateBackground()
        {
            //var spriteBackground = SpriteFactory.Instance.GetSprite("star_background.png");
            //var spriteMidground = SpriteFactory.Instance.GetSprite("star_midground.png");
            //var spriteForeground = SpriteFactory.Instance.GetSprite("star_foreground.png");

            //var state = new RenderStates()
            //{
            //    BlendMode = BlendMode.Alpha,
            //    Transform = Transform.Identity
            //};

            //state.Transform.Translate(0, 0);

            _spriteBackground = Parent.Graphic.AddSprite(EnumLayer.Background, 0, 0, Color.White, "star_background.png", Parent.Graphic.GetWidth()*2, Parent.Graphic.GetHeight()*2, true);
            _spriteMidground = Parent.Graphic.AddSprite(EnumLayer.Background, 0, 0, Color.White, "star_midground.png", Parent.Graphic.GetWidth() * 2, Parent.Graphic.GetHeight() * 2, true);
            _spriteForeground = Parent.Graphic.AddSprite(EnumLayer.Background, 0, 0, Color.White, "star_foreground.png", Parent.Graphic.GetWidth() * 2, Parent.Graphic.GetHeight() * 2, true);

            
        }

        private void CreateMenu()
        {
            var sprite = SpriteFactory.Instance.GetSpriteAnimated("button.png", 186, 52, 0, 0, 2, false, false,
                186, 52);
            sprite.Color = Color.Green;

            var state = new RenderStates()
            {
                BlendMode = BlendMode.Alpha,
                Transform = Transform.Identity
            };

            state.Transform.Translate(200, 100);

            var spriteObject = new SpriteObject(sprite, state, ButtonHoverStart, ButtonHoverEnd, ButtonClick);

            spriteObject.Sprite.SetAnimation(1, 1, false, false);


            Parent.Graphic.AddSpriteObject(EnumLayer.Ui, spriteObject);
        }

        public void ButtonClick(SpriteObject obj)
        {
            //obj.Sprite.SetFrame(2);
            obj.Sprite.SetAnimation(2, 2, false, false);
        }

        public void ButtonHoverStart(SpriteObject obj)
        {
            //obj.Sprite.SetFrame(1);
            obj.Sprite.SetAnimation(0, 0, false, false);
        }

        public void ButtonHoverEnd(SpriteObject obj)
        {
            //obj.Sprite.SetFrame(0);
            obj.Sprite.SetAnimation(1, 1, false, false);
        }
    }
}
