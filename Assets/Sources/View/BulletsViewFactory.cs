using System;
using Asteroids.Model.Sources.Model.Weapon;
using UnityEngine;

namespace Sources.View
{
    public class BulletsViewFactory : TransformableViewFactory<Bullet>
    {
        [SerializeField] private TransformableView _defaultBulletTemplate;
        [SerializeField] private TransformableView _laserBulletTemplate; 

        protected override TransformableView GetTemplate(Bullet bullet)
        {
            if (bullet is DefaultBullet)
                return _defaultBulletTemplate;
            else if (bullet is LaserGunBullet)
                return _laserBulletTemplate;

            throw new InvalidOperationException();
        }
    }
}
