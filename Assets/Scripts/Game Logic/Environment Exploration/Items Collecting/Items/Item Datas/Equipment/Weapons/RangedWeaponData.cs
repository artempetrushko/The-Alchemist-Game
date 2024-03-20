using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeaponData : WeaponData
{
    [Header("ѕараметры оружи€ дальнего бо€")]
    [SerializeField]
    protected int missileFlightSpeed;
    [Header("—нар€д")]
    [SerializeField]
    protected Projectile projectile;
    [Header("Ёффект попадани€")]
    [SerializeField]
    protected GameObject effect;
    [Header("Ёффект выстрела")]
    [SerializeField]
    protected GameObject shootEffect;
    [Header("–азрушение при попадании")]
    [SerializeField]
    protected bool destroyOnCollision;

    public int BaseMissileFlightSpeed => missileFlightSpeed;

    public Projectile Projectile => projectile;
    public GameObject Effect => effect;
    public GameObject ShootEffect => shootEffect;
    public bool DestroyOnCollision => destroyOnCollision;
}
