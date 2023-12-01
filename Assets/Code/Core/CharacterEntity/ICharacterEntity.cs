using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.CharacterEntity
{
    public interface ICharacterEntity
    {
        string GetName();
        
        string GetStats();

        Sprite GetCharacterIcon();

        List<Sprite> GetItemsIcon();

        void AddItem(int aItemId, int aSlotId);

        int[] GetItemsId(); 

        void GetPotion(out Sprite aIcon, out Action oninvokePotion);

        Action<ICharacterEntity> GetAttack();

        void OnAttack(Action aOnAttack);
        
        void UpdateStats(Dictionary<string, int> stats);

        void ReceiveDamage(int aDamage);

        GameObject GetGameObject(); 
    }
}