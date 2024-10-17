using System.Collections.Generic;
using System.Linq;
using Polyperfect.Common;
using Polyperfect.Crafting.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Polyperfect.Crafting.Integration.UGUI
{
    [RequireComponent(typeof(Crafter))]
    public class UGUICrafter : ItemUserBase
    {
        [SerializeField] List<RecipeCategoryObject> MadeCategories = new();
        [SerializeField] public ChildSlotsInventory StartingInventory;
        [SerializeField] public ChildSlotsInventory StartingOutput;
        [SerializeField] ChildConstructor PossibilitiesConstructor;
        [SerializeField] RecipeDisplay RecipeDisplay;
        [SerializeField] Text RecipeNameText;
        Crafter crafter;
        string startingText;
        public override string __Usage => "Easy crafting";
        void Awake()
        {
            crafter = GetComponent<Crafter>();
            crafter.OnSourceUpdate.AddListener(UpdateCraftables);
            if (crafter.Source == null) crafter.Source = StartingInventory;
            if (crafter.Destination == null) crafter.Destination = StartingOutput;
            if (RecipeNameText)
                startingText = RecipeNameText.text;
        }

        protected void Start()
        {
            UpdateCraftables();
        }

        void OnDisable()
        {
            crafter.Recipe = null;
            if (RecipeNameText)
                RecipeNameText.text = startingText;
        }

        IEnumerable<RuntimeID> GetRelevantRecipes()
        {
            return World.RecipeIDs.Where(
                recipeID => MadeCategories.Any(category => World.CategoryContains(category, recipeID)));
        }

        public IEnumerable<RuntimeID> GetCraftableRecipesGivenInventory(IEnumerable<ItemStack> inventory)
        {
            if (inventory == null)
                yield break;

            foreach (var recipeID in GetRelevantRecipes())
            {
                var recipe = World.GetRecipeInputs(recipeID);
                var craftAmount = crafter.GetMaxCraftAmount(inventory, recipe);
                if (craftAmount > 0)
                    yield return recipeID;
            }
        }

        public void OnRecipeSelected(RuntimeID recipeID)
        {
            crafter.Recipe = new SimpleRecipe(World.GetRecipeInputs(recipeID), World.GetRecipeOutputs(recipeID));
            if (RecipeNameText)
                RecipeNameText.text = World.GetName(recipeID);

            RecipeDisplay.DisplayRecipe(recipeID);
        }

        void UpdateCraftables()
        {
            if (crafter.Source == null)
            {
                PossibilitiesConstructor.ClearConstructed();
                return;
            }

            var craftableRecipesGivenInventory = GetCraftableRecipesGivenInventory(crafter.Source.Peek());
            PossibilitiesConstructor.Construct(
                craftableRecipesGivenInventory,
                (go, recipeID) =>
                {
                    var evt = new EventTrigger.TriggerEvent();
                    evt.AddListener(e => OnRecipeSelected(recipeID));
                    go.AddOrGetComponent<EventTrigger>().triggers.Add(
                        new EventTrigger.Entry { callback = evt, eventID = EventTriggerType.PointerClick });

                    go.GetComponent<ChildConstructor>().ConstructAndInsertItems(World.GetRecipeOutputs(recipeID));
                });
        }
    }
}
