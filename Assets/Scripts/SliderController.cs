using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private CharacterController.Stats sliderType;

    private VictorController victorController;

    private void OnEnable()
    {
        VictorController.OnPersonajeInstanciado += HandlePersonajeInstanciado;
    }

    private void OnDisable()
    {
        if (victorController != null)
        {
            VictorController.OnPersonajeInstanciado -= HandlePersonajeInstanciado;
            victorController.OnStatsChanged -= HandeStatsChanged;
        }
    }

    private void HandlePersonajeInstanciado(VictorController vc)
    {
        // Guardar la referencia al PersonajeController y suscribirse a su evento de daño
        victorController = vc;
        victorController.OnStatsChanged += HandeStatsChanged;

        switch (sliderType)
        {
            case CharacterController.Stats.LifePoints:
                InitializeBarStat(vc.GetStats().life, vc.GetStats().maxLife);
                break;

            case CharacterController.Stats.Shield:
                InitializeBarStat(vc.GetStats().shield, vc.GetStats().maxShield);
                break;

            case CharacterController.Stats.characterSpeed:
                InitializeBarStat(vc.GetStats().movementSpeed, vc.GetStats().maxMovementSpeed);
                break;

            case CharacterController.Stats.attackSpeed:
                InitializeBarStat(vc.GetStats().attackSpeed, vc.GetStats().maxAttackSpeed);
                break;        
        }
        
    }

    private void HandeStatsChanged(VictorController.VictorStats stats)
    {
        switch (sliderType)
        {
            case CharacterController.Stats.LifePoints:
                ChangeActualValue(stats.life);
                break;

            case CharacterController.Stats.Shield:
                ChangeActualValue(stats.shield);
                break;

            case CharacterController.Stats.characterSpeed:
                ChangeActualValue(stats.movementSpeed);
                break;

            case CharacterController.Stats.attackSpeed:
                ChangeActualValue(stats.attackSpeed);
                break;
        }
    }
    public void InitializeBarStat(float actualValue, float maxValue)
    {
        SetMaxValue(maxValue);
        ChangeActualValue(actualValue);
    }

    public void SetMaxValue (float maxValue)
    {
        slider.maxValue = maxValue;
    }

    public void ChangeActualValue(float actualValue)
    {
        slider.value = actualValue;
    }
}
