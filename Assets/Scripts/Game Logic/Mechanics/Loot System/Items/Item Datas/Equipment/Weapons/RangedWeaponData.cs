using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeaponData : WeaponData
{
    [Header("��������� ������ �������� ���")]
    [SerializeField]
    protected int missileFlightSpeed;
    [Header("������")]
    [SerializeField]
    protected Projectile projectile;
    [Header("������ ���������")]
    [SerializeField]
    protected GameObject effect;
    [Header("������ ��������")]
    [SerializeField]
    protected GameObject shootEffect;
    [Header("���������� ��� ���������")]
    [SerializeField]
    protected bool destroyOnCollision;

    public int BaseMissileFlightSpeed => missileFlightSpeed;

    public Projectile Projectile => projectile;
    public GameObject Effect => effect;
    public GameObject ShootEffect => shootEffect;
    public bool DestroyOnCollision => destroyOnCollision;
}
