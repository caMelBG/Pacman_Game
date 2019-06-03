namespace PacMan.Factories
{
    using PacMan.Models.Abstract;
    using PacMan.Models.Common;
    using PacMan.Models.Dots;
    using PacMan.Models.Enums;
    using System;
    using System.Collections.Generic;

    public class DotFactory
    {
        private IDictionary<DotTypes, Dot> _instances;

        public DotFactory()
        {
            this._instances = new Dictionary<DotTypes, Dot>();
        }

        public Dot CreateDot(DotTypes type, Position position)
        {
            Dot instance = null;
            if (!_instances.TryGetValue(type, out instance))
            {
                switch (type)
                {
                    case DotTypes.Regular:
                        instance = new RegularDot(position);
                        break;
                    case DotTypes.Heavy:
                        instance = new HeavyDot(position);
                        break;
                    default:
                        throw new ArgumentException($"Not supported dot type {type}");
                }
                _instances.Add(type, instance);
            }
            instance.Position = position;
            instance.RecreateFigure();
            return instance;
        }
    }
}
