using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public interface IHittable
{
    bool OnHit(float dmg);

    void OnDeath();
}
