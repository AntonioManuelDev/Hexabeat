using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameController : MonoBehaviour
{
    public GameObject circlePrefab; // Prefab del círculo que aparecerá en la línea de ritmo
    public GameObject circleLPrefab;
    public GameObject circleWPrefab;
    public float spawnInterval = 1.0f;
    public float moveSpeed = 400f;
    public float spawnIntervalL = 4.0f;
    public float moveSpeedL = 200f;
    public float spawnIntervalW = 8.0f;
    public float moveSpeedW = 600f;
    public float destroyPositionX = 450f;
    public float penaltyTime = 2f;

    private List<GameObject> circles = new List<GameObject>();
    private List<GameObject> circlesL = new List<GameObject>();
    private List<GameObject> circlesW = new List<GameObject>();
    private Shoot shootWeapon; // Referencia al componente Shoot
    private ShootBeam shootBeam;
    private ShootWave shootWave;
    private GameObject center;
    public bool penaltyActive = false;
    //private bool canShoot = true;
    public bool penalize = false;

    void Start()
    {
        // Obtener referencias a los componentes necesarios
        shootWeapon = GameObject.Find("ShotWeapon").GetComponent<Shoot>();
        shootBeam = GameObject.Find("ShotBeam").GetComponent<ShootBeam>();
        shootWave = GameObject.Find("ShotWave").GetComponent<ShootWave>();
        center = GameObject.Find("center");

        // Generar un círculo cada cierto intervalo de tiempo
        StartCoroutine(SpawnCircleRoutine());
        StartCoroutine(SpawnCircleLRoutine());
        StartCoroutine(SpawnCircleWRoutine());
    }

    IEnumerator SpawnCircleRoutine()
    {
        while (true)
        {
            // Crear un nuevo círculo (nota) y agregarlo a la lista de círculos
            GameObject newCircle = Instantiate(circlePrefab, transform);
            RectTransform RTnewCircle = newCircle.GetComponent<RectTransform>();
            RTnewCircle.anchoredPosition = new Vector2(-450f, 0f); // Posición inicial

            circles.Add(newCircle); // Agregar el círculo a la lista

            yield return new WaitForSeconds(spawnInterval); // Esperar antes de crear el siguiente círculo
            //canShoot = true;
        }
    }
    IEnumerator SpawnCircleLRoutine()
    {
        while (true)
        {
            // Crear un nuevo círculo (nota) y agregarlo a la lista de círculos
            GameObject newCircleL = Instantiate(circleLPrefab, transform);
            RectTransform RTnewCircleL = newCircleL.GetComponent<RectTransform>();
            RTnewCircleL.anchoredPosition = new Vector2(-450f, 0f); // Posición inicial

            circlesL.Add(newCircleL); // Agregar el círculo a la lista

            yield return new WaitForSeconds(spawnIntervalL); // Esperar antes de crear el siguiente círculo
            //canShoot = true;
        }
    }
    IEnumerator SpawnCircleWRoutine()
    {
        while (true)
        {
            // Crear un nuevo círculo (nota) y agregarlo a la lista de círculos
            GameObject newCircleW = Instantiate(circleWPrefab, transform);
            RectTransform RTnewCircleW = newCircleW.GetComponent<RectTransform>();
            RTnewCircleW.anchoredPosition = new Vector2(-450f, 0f); // Posición inicial

            circlesW.Add(newCircleW); // Agregar el círculo a la lista

            yield return new WaitForSeconds(spawnIntervalW); // Esperar antes de crear el siguiente círculo
            //canShoot = true;
        }
    }

    void Update()
    {
        // Verificar si se pulsa la tecla Espacio y si hay círculos en la lista
        if (Input.GetKeyDown(KeyCode.Space) && !penaltyActive)
        {
            penalize = true;
            // Verificar cada círculo en la lista
            for (int i = circles.Count - 1; i >= 0; i--)
            {
                GameObject circle = circles[i];
                RectTransform RTcircle = circle.GetComponent<RectTransform>();
                //Debug.Log($"Touched: {centerCollider.IsTouched()}, X Position: {RTcircle.anchoredPosition.x}");

                // Verificar si este círculo está en contacto con el BoxCollider2D "centerCollider"

                if (RTcircle.anchoredPosition.x > -30f && RTcircle.anchoredPosition.x < 30f)
                {
                    shootWeapon.shootBullet();
                    //canShoot = false;
                    Destroy(circle);
                    circles.RemoveAt(i);
                    i--;
                    penalize = false;
                }
            }
            // Verificar cada círculo en la lista
            for (int i = circlesL.Count - 1; i >= 0; i--)
            {
                GameObject circleL = circlesL[i];
                RectTransform RTcircleL = circleL.GetComponent<RectTransform>();
                //Debug.Log($"Touched: {centerCollider.IsTouched()}, X Position: {RTcircle.anchoredPosition.x}");

                // Verificar si este círculo está en contacto con el BoxCollider2D "centerCollider"
                if (RTcircleL.anchoredPosition.x > -30f && RTcircleL.anchoredPosition.x < 30f)
                {
                    shootBeam.shootBeam();
                    //canShoot = false;
                    Destroy(circleL);
                    circlesL.RemoveAt(i);
                    i--;
                    penalize = false;
                }
            }
            for (int i = circlesW.Count - 1; i >= 0; i--)
            {
                GameObject circleW = circlesW[i];
                RectTransform RTcircleW = circleW.GetComponent<RectTransform>();
                //Debug.Log($"Touched: {centerCollider.IsTouched()}, X Position: {RTcircle.anchoredPosition.x}");

                // Verificar si este círculo está en contacto con el BoxCollider2D "centerCollider"
                if (RTcircleW.anchoredPosition.x > -30f && RTcircleW.anchoredPosition.x < 30f)
                {
                    shootWave.waveGrowth();
                    //canShoot = false;
                    Destroy(circleW);
                    circlesW.RemoveAt(i);
                    i--;
                    penalize = false;
                }
            }
        }
            
        if(!penaltyActive && penalize)           
        {               
            StartCoroutine(TakePenalty());            
        }

        // Mover y verificar cada círculo en la lista
        for (int i = 0; i < circles.Count; i++)
        {
            GameObject circle = circles[i];
            RectTransform RTcircle = circle.GetComponent<RectTransform>();

            // Mover el círculo hacia la derecha
            RTcircle.anchoredPosition += Vector2.right * moveSpeed * Time.deltaTime;

            // Verificar si el círculo debe ser destruido
            if (RTcircle.anchoredPosition.x >= destroyPositionX)
            {
                Destroy(circle); // Destruir el GameObject del círculo
                circles.RemoveAt(i); // Eliminar el círculo de la lista
                i--; // Decrementar el índice para evitar omitir el siguiente círculo
            }
        }        
        
        for (int i = 0; i < circlesL.Count; i++)
        {
            GameObject circleL = circlesL[i];
            if (circleL != null)
            {
                RectTransform RTcircleL = circleL.GetComponent<RectTransform>();
                // Mover el círculo hacia la derecha
                RTcircleL.anchoredPosition += Vector2.right * moveSpeedL * Time.deltaTime;

                // Verificar si el círculo debe ser destruido
                if (RTcircleL.anchoredPosition.x >= destroyPositionX)
                {
                    Destroy(circleL); // Destruir el GameObject del círculo
                    circlesL.RemoveAt(i); // Eliminar el círculo de la lista
                    i--; // Decrementar el índice para evitar omitir el siguiente círculo
                }
            }
        }
        for (int i = 0; i < circlesW.Count; i++)
        {
            GameObject circleW = circlesW[i];
            if (circleW != null)
            {
                RectTransform RTcircleW = circleW.GetComponent<RectTransform>();
                // Mover el círculo hacia la derecha
                RTcircleW.anchoredPosition += Vector2.right * moveSpeedW * Time.deltaTime;

                // Verificar si el círculo debe ser destruido
                if (RTcircleW.anchoredPosition.x >= destroyPositionX)
                {
                    Destroy(circleW); // Destruir el GameObject del círculo
                    circlesW.RemoveAt(i); // Eliminar el círculo de la lista
                    i--; // Decrementar el índice para evitar omitir el siguiente círculo
                }
            }
        }
    }

    IEnumerator TakePenalty()
    {
        penaltyActive = true;
        center.SetActive(false);
        yield return new WaitForSeconds(penaltyTime);
        center.SetActive(true);
        penaltyActive = false; // Restablecer la bandera de penalización al finalizar
        penalize = false;
    }
}