using UnityEngine;
using UnityEngine.SceneManagement;
using static JournalController;
using static VictorController;

public class VictorSettings : MonoBehaviour
{
    public VictorStats victorStats;
    public JournalStats journalStats;

    private void OnEnable()
    {
        VictorController.OnPersonajeInstanciado += HandlePersonajeInstanciado;
    }

    private void OnDisable()
    {
        VictorController.OnPersonajeInstanciado -= HandlePersonajeInstanciado;
    }
    
    public static VictorSettings Instance { get; private set; }
   
    private void HandlePersonajeInstanciado(VictorController vc)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Laboratorio":
                victorStats = VictorController.GetDefaultStats();
                journalStats = JournalController.GetDefaultStats();
                break;
        }
    }

    public void UpdateVictorStats(VictorStats newVictorStats)
    {
        victorStats = newVictorStats;
    }

    public void UpdateJournalStats(JournalStats newJournalStats)
    {
        journalStats = newJournalStats;
    }

    private void Awake()
    {
        // Si ya hay una instancia y no es esta, destruye este objeto.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Si esta es la primera instancia, asígnala y marca este objeto para no destruirlo al cargar una nueva escena.
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
