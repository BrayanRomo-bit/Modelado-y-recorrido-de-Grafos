# Reporte Formal de Practica: Modelado y Recorrido de Grafos

| Campo | Detalle |
| :--- | :--- |
| **Actividad** | Tarea 4.3: Practica de aplicacion - Modelado y recorrido de Grafos |
| **Problema Modelado** | Red de distribucion logistica regional mediante grafo ponderado no dirigido |
| **Algoritmo Aplicado** | Algoritmo de Dijkstra (camino mas corto desde origen unico) |
| **Plataforma** | C# / .NET 10 Windows Forms |
| **Fecha** | 26 de noviembre de 2026 |

---

## 1. Resumen Ejecutivo

El presente documento describe la formulacion matematica, el modelado computacional, la representacion estructural y los resultados de optimizacion obtenidos al resolver el problema de ruta minima sobre una red de distribucion regional de 7 ciudades conectadas por 10 tramos carreteros ponderados por distancia en kilometros.

La solucion de software permite tanto analizar la red regional precargada como construir grafos personalizados desde cero, visualizar la matriz y lista de adyacencia en tiempo real, y auditar paso a paso la traza de ejecucion del algoritmo de Dijkstra.

---

## 2. Formulacion Matematica del Problema

La red logistica se modela como un grafo ponderado no dirigido:

$$G = (V, E, w)$$

donde:

- $V = \{\text{CDMX}, \text{Puebla}, \text{Veracruz}, \text{Queretaro}, \text{Leon}, \text{Guadalajara}, \text{Monterrey}\}$, con $|V| = 7$ vertices.
- $E \subseteq \{\{u, v\} \mid u, v \in V, u \neq v\}$, con $|E| = 10$ aristas bidireccionales.
- $w: E \to \mathbb{R}^+$ asigna a cada enlace carretero su distancia en kilometros ($w(u, v) > 0$).

Dado un nodo origen $s \in V$ y un nodo destino $t \in V$, el problema de optimizacion consiste en encontrar una trayectoria simple $P = (v_0, v_1, \dots, v_k)$ con $v_0 = s$ y $v_k = t$ que minimice el costo acumulado:

$$\min_{P} \; C(P) = \sum_{i=0}^{k-1} w(v_i, v_{i+1})$$

sujeto a $(v_i, v_{i+1}) \in E$ para todo $0 \le i < k$, y $d(v) \ge 0$ para todo $v \in V$.

---

## 3. Modelado de la Red de Distribucion

### 3.1 Matriz de Adyacencia

La matriz $M \in (\mathbb{R}^+ \cup \{0, \infty\})^{7 \times 7}$ representa los pesos directos entre cada par de nodos. Debido a que el grafo es no dirigido, la matriz es simetrica ($M_{i,j} = M_{j,i}$) y su diagonal principal es nula ($M_{i,i} = 0$). El valor `INF` denota ausencia de conexion directa.

| Nodo | CDMX | Puebla | Veracruz | Queretaro | Leon | Guadalajara | Monterrey |
| :--- | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **CDMX** | 0 | 130 | INF | 215 | INF | 545 | INF |
| **Puebla** | 130 | 0 | 275 | INF | INF | INF | INF |
| **Veracruz** | INF | 275 | 0 | 530 | INF | INF | INF |
| **Queretaro** | 215 | INF | 530 | 0 | 170 | INF | 710 |
| **Leon** | INF | INF | INF | 170 | 0 | 220 | 690 |
| **Guadalajara** | 545 | INF | INF | INF | 220 | 0 | 790 |
| **Monterrey** | INF | INF | INF | 710 | 690 | 790 | 0 |

### 3.2 Lista de Adyacencia

Cada vertice mantiene la lista ordenada de sus vecinos alcanzables en un salto junto con el peso de la arista correspondiente:

| Vertice | Vecinos Directos (Destino y Distancia) |
| :--- | :--- |
| **CDMX** | Guadalajara (545 km), Puebla (130 km), Queretaro (215 km) |
| **Puebla** | CDMX (130 km), Veracruz (275 km) |
| **Veracruz** | Puebla (275 km), Queretaro (530 km) |
| **Queretaro** | CDMX (215 km), Leon (170 km), Monterrey (710 km), Veracruz (530 km) |
| **Leon** | Guadalajara (220 km), Monterrey (690 km), Queretaro (170 km) |
| **Guadalajara** | CDMX (545 km), Leon (220 km), Monterrey (790 km) |
| **Monterrey** | Guadalajara (790 km), Leon (690 km), Queretaro (710 km) |

---

## 4. Traza de Ejecucion del Algoritmo de Dijkstra

### 4.1 Caso de Estudio Principal: Origen = `CDMX`, Destino = `Monterrey`

En cada iteracion el algoritmo selecciona el vertice no visitado con menor distancia acumulada, lo marca como definitivo y relaja sus aristas salientes.

| Paso | Nodo Seleccionado | Costo Acumulado | Vector de Distancias (km) | Accion Realizada |
| :---: | :--- | :---: | :--- | :--- |
| 0 | CDMX | 0 km | CDMX=0, Puebla=INF, Veracruz=INF, Queretaro=INF, Leon=INF, Guadalajara=INF, Monterrey=INF | Inicializacion: distancia(CDMX) = 0, resto = INF. |
| 1 | CDMX | 0 km | CDMX=0, Puebla=130, Veracruz=INF, Queretaro=215, Leon=INF, Guadalajara=545, Monterrey=INF | Visita CDMX (0 km). Relaja: Guadalajara: INF -> 545 (via CDMX); Puebla: INF -> 130 (via CDMX); Queretaro: INF -> 215 (via CDMX). |
| 2 | Puebla | 130 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=INF, Guadalajara=545, Monterrey=INF | Visita Puebla (130 km). Relaja: Veracruz: INF -> 405 (via Puebla). |
| 3 | Queretaro | 215 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=385, Guadalajara=545, Monterrey=925 | Visita Queretaro (215 km). Relaja: Leon: INF -> 385 (via Queretaro); Monterrey: INF -> 925 (via Queretaro). |
| 4 | Leon | 385 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=385, Guadalajara=545, Monterrey=925 | Visita Leon (385 km). Sin mejoras en vecinos. |
| 5 | Veracruz | 405 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=385, Guadalajara=545, Monterrey=925 | Visita Veracruz (405 km). Sin mejoras en vecinos. |
| 6 | Guadalajara | 545 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=385, Guadalajara=545, Monterrey=925 | Visita Guadalajara (545 km). Sin mejoras en vecinos. |
| 7 | Monterrey | 925 km | CDMX=0, Puebla=130, Veracruz=405, Queretaro=215, Leon=385, Guadalajara=545, Monterrey=925 | Visita Monterrey (925 km). Destino alcanzado. |

### 4.2 Caso Secundario: Origen = `Puebla`, Destino = `Guadalajara`

- Ruta optima calculada: `Puebla -> CDMX -> Guadalajara`
- Distancia total minima: **675 km** (`130 km + 545 km`)
- Ruta alternativa descartada: `Puebla -> CDMX -> Queretaro -> Leon -> Guadalajara` (**735 km**)

---

## 5. Resultados de Optimizacion

| Trayecto Evaluado | Ruta Optima Obtenida | Distancia Minima | Alternativa Suboptima Comparada | Ahorro Logrado |
| :--- | :--- | :---: | :--- | :---: |
| `CDMX` -> `Monterrey` | `CDMX -> Queretaro -> Monterrey` | **925 km** | `CDMX -> Queretaro -> Leon -> Monterrey` (1075 km) | 150 km (13.95%) |
| `CDMX` -> `Monterrey` | `CDMX -> Queretaro -> Monterrey` | **925 km** | `CDMX -> Guadalajara -> Monterrey` (1335 km) | 410 km (30.71%) |
| `Puebla` -> `Guadalajara` | `Puebla -> CDMX -> Guadalajara` | **675 km** | `Puebla -> CDMX -> Queretaro -> Leon -> Guadalajara` (735 km) | 60 km (8.16%) |

El algoritmo garantiza optimalidad global al operar sobre pesos estrictamente positivos ($w(e) > 0$) y converge en $O(|V|^2)$ operaciones sobre la representacion actual.

---

## 6. Arquitectura de la Solucion de Software

El proyecto se estructuro de forma modular por responsabilidad en carpetas y sub-namespaces:

| Carpeta / Archivo | Responsabilidad Principal |
| :--- | :--- |
| `Modelos/NodoGrafo.cs` | Representacion de cada vertice con identificador, nombre y coordenadas normalizadas. |
| `Modelos/AristaGrafo.cs` | Representacion de enlaces ponderados con validacion de peso positivo. |
| `Modelos/GrafoPonderado.cs` | Gestion de nodos y aristas, generacion de matriz/lista de adyacencia y carga del ejemplo regional. |
| `Algoritmos/PasoDijkstra.cs` | Instantanea de cada iteracion del algoritmo para construir la traza de ejecucion. |
| `Algoritmos/ResultadoDijkstra.cs` | Encapsulamiento de la ruta optima, costo total, vector de distancias y traza completa. |
| `Algoritmos/AlgoritmoDijkstra.cs` | Implementacion del algoritmo de Dijkstra y formateo del vector de distancias. |
| `Renderizado/RenderizadorGrafo.cs` | Dibujo vectorial responsivo, resaltado de ruta optima y soporte para arrastrar nodos con el mouse. |
| `Pruebas/PruebasGrafo.cs` | Bateria de pruebas unitarias automatizadas invocables por linea de comandos (`--test`). |
| `Form1.Designer.cs` / `Form1.cs` | Interfaz grafica responsiva disenada en `Form1.cs [Design]`, creacion desde cero y carga de ejemplo. |

---

## 7. Verificacion y Pruebas Automatizadas

Ejecucion verificada mediante `dotnet run -- --test`:

| # | Caso de Prueba | Resultado |
| :---: | :--- | :---: |
| 1 | Carga de red regional y conteo de nodos/aristas (7 nodos, 10 aristas) | PASS |
| 2 | Simetria y valores de matriz de adyacencia | PASS |
| 3 | Consistencia de la lista de adyacencia | PASS |
| 4 | Dijkstra `CDMX` a `Monterrey` (925 km, ruta de 3 nodos) | PASS |
| 5 | Dijkstra `Puebla` a `Guadalajara` (675 km, ruta de 3 nodos) | PASS |
| 6 | Deteccion de nodo desconectado sin ruta (distancia infinita) | PASS |
| 7 | Actualizacion de arista y eliminacion de nodo | PASS |

**Resumen:** 7 pruebas ejecutadas, 7 exitosas, 0 fallidas.
