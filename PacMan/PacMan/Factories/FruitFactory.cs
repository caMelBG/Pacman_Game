namespace PacMan.Factories
{
    using Models.Abstract;
    using Models.Common;
    using PacMan.Factories.Builders.FruitBuilders;
    using PacMan.Models.Enums;
    using System;

    public class FruitFactory
    {
        private FruitBuilder _fruitBuilder;
        
        public Fruit CreateFruit(FruitTypes type, Position position)
        {
            switch (type)
            {
                case FruitTypes.Apple:
                    _fruitBuilder = new AppleBuilder(position);
                    break;
                case FruitTypes.Cherry:
                    _fruitBuilder = new CherryBuilder(position);
                    break;
                case FruitTypes.Grapes:
                    _fruitBuilder = new GrapesBuilder(position);
                    break;
                case FruitTypes.Lemon:
                    _fruitBuilder = new LemonBuilder(position);
                    break;
                case FruitTypes.Orange:
                    _fruitBuilder = new OrangeBuilder(position);
                    break;
                case FruitTypes.Strawberry:
                    _fruitBuilder = new StrawberryBuilder(position);
                    break;
                default:
                    throw new ArgumentException($"Not supported fruit type {type}");
            }

            return _fruitBuilder.Instance;
        }
    }
}
