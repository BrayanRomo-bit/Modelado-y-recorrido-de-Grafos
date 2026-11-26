# Reporte Formal - Tarea 4.3: Modelado y Recorrido de Grafos

## 1. Formulacion del Problema

Se modela una red de distribucion logistica regional en Mexico mediante un grafo ponderado no dirigido $G = (V, E, w)$, donde:

- $V$ es el conjunto de 7 nodos que representan centros de distribucion: `CDMX`, `Puebla`, `Veracruz`, `Queretaro`, `Leon`, `Guadalajara` y `Monterrey`.
- $E$ es el conjunto de 10 aristas bidireccionales que representan las rutas carreteras disponibles entre ciudades.
- $w: E \to \mathbb{R}^+$ asigna a cada arista su distancia en kilometros (peso positivo).

El objetivo de optimizacion consiste en determinar la ruta de menor costo acumulado (distancia minima en kilometros) entre un nodo origen $s \in V$ y un nodo destino $t \in V$, utilizando el **Algoritmo de Dijkstra**.

Formalmente, se busca minimizar:

$$\min_{P = (v_0, v_1, \dots, v_k)} \sum_{i=0}^{k-1} w(v_i, v_{i+1})$$

sujeto a $v_0 = s$, $v_k = t$, y $(v_i, v_{i+1}) \in E$.

---

## 2. Representacion del Grafo

### 2.1 Matriz de Adyacencia

La matriz $M \in (\mathbb{R}^+ \cup \{0, \infty\})^{7 \times 7}$ almacena el peso directo entre cada par de ciudades (`INF` indica ausencia de conexion directa):

| Nodo | CDMX | Puebla | Veracruz | Queretaro | Leon | Guadalajara | Monterrey |
| :--- | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **CDMX** | 0 | 130 | INF | 215 | INF | 545 | INF |
| **Puebla** | 130 | 0 | 275 | INF | INF | INF | INF |
| **Veracruz** | INF | 275 | 0 | 530 | INF | INF | INF |
| **Queretaro** | 215 | INF | 530 | 0 | 170 | INF | 710 |
| **Leon** | INF | INF | INF | 170 | 0 | 220 | 690 |
| **Guadalajara** | 545 | INF | INF | INF | 220 | 0 | 790 |
| **Monterrey** | INF | INF | INF | 710 | 690 | 790 | 0 |

### 2.2 Lista de Adyacencia

- **CDMX** -> Guadalajara (545 km), Puebla (130 km), Queretaro (215 km)
- **Puebla** -> CDMX (130 km), Veracruz (275 km)
- **Veracruz** -> Puebla (275 km), Queretaro (530 km)
- **Queretaro** -> CDMX (215 km), Leon (170 km), Monterrey (710 km), Veracruz (530 km)
- **Leon** -> Guadalajara (220 km), Monterrey (690 km), Queretaro (170 km)
- **Guadalajara** -> CDMX (545 km), Leon (220 km), Monterrey (790 km)
- **Monterrey** -> Guadalajara (790 km), Leon (690 km), Queretaro (710 km)

---

## 3. Traza de Ejecucion del Algoritmo de Dijkstra

### Caso de Estudio: Origen = `CDMX`, Destino = `Monterrey`

| Paso | Nodo Actual | Costo Acumulado | Vector de Distancias (km) | Accion Realizada |
| :---: | :---: | :---: | :--- | :--- |
| 0 | CDMX | 0 km | CDMX=0, Puebla=INF, Veracruz=INF, Queretaro=INF, Leon=INF, Guadalajara=INF, Monterrey=INF | Inicializacion: distancia(CDMX) = 0, resto = INF. |
| 1 | CDMX | 0 km | CDMX=0, Puebla=130, Veracruz=INF, Queretaro=215, Leon=INF, Guadalajara=545, Monterrey=INF | Visita CDMX (0 km). Relaja: Guadalajara: INF -> 545 (via CDMX); Puebla: INF -> 130 (via CDMX); Queretaro: INF -> 215 (via CDMX). |
| 2 | Puebla | 130 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=INF, Guadalajara=545, Monterrey=INF | Visita Puebla (130 km). Relaja: Veracruz: INF -> 405 (via Puebla). |
| 3 | Queretaro | 215 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=385, Guadalajara=545, Monterrey=925 | Visita Queretaro (215 km). Relaja: Leon: INF -> 385 (via Queretaro); Monterrey: INF -> 925 (via Queretaro). |
| 4 | Leon | 385 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=385, Guadalajara=545, Monterrey=925 | Visita Leon (385 km). Sin mejoras en vecinos. |
| 5 | Veracruz | 405 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=385, Guadalajara=545, Monterrey=925 | Visita Veracruz (405 km). Sin mejoras en vecinos. |
| 6 | Guadalajara | 545 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=385, Guadalajara=545, Monterrey=925 | Visita Guadalajara (545 km). Sin mejoras en vecinos. |
| 7 | Monterrey | 925 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=385, Guadalajara=545, Monterrey=925 | Visita Monterrey (925 km). Destino alcanzado. |

---

## 4. Resultados de Optimizacion y Pruebas

### 4.1 Resultados de Optimizacion

- **Ruta optima `CDMX` -> `Monterrey`**: `CDMX -> Queretaro -> Monterrey` con una distancia total de **925 km** (superando alternativas como `CDMX -> Queretaro -> Leon -> Monterrey` de 1075 km y `CDMX -> Guadalajara -> Monterrey` de 1335 km).
- **Ruta optima `Puebla` -> `Guadalajara`**: `Puebla -> CDMX -> Guadalajara` con una distancia total de **675 km** (superando la ruta por Queretaro y Leon de 735 km).

### 4.2 Arquitectura de la Solucion de Software

La aplicacion Windows Forms (.NET 10) implementa separacion de responsabilidades y diseno responsivo:

- `NodoGrafo`, `AristaGrafo`, `GrafoPonderado`: modelado del grafo, validaciones y generacion de matriz y lista de adyacencia.
- `PasoDijkstra`, `ResultadoDijkstra`, `AlgoritmoDijkstra`: ejecucion de Dijkstra con captura completa de traza.
- `RenderizadorGrafo`: dibujo vectorial escalable que adapta posiciones y tamanos al redimensionar la ventana.
- `Form1` y `Form1.Designer.cs`: interfaz grafica responsiva con paneles ajustables (`SplitContainer`), tabla de traza, tabla de matriz de adyacencia y visor de lista de adyacencia.

### 4.3 Verificacion Automatizada

Ejecutado mediante `dotnet run -- --test`:

1. Carga de red regional y conteo de nodos/aristas: PASS
2. Simetria y valores de matriz de adyacencia: PASS
3. Lista de adyacencia consistente: PASS
4. Dijkstra CDMX a Monterrey (925 km): PASS
5. Dijkstra Puebla a Guadalajara (675 km): PASS
6. Nodo desconectado sin ruta: PASS
7. Actualizacion de arista y eliminacion de nodo: PASS

Resultado: **7 de 7 pruebas exitosas**.
