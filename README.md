# BarCat Reader
![preview](/preview.png)

WEB-застосунок для демонстрації роботи з HTTP API на прикладі декодування штрих-кодів.

Дозволяє працювати як через WEB-інтерфейс, так і напряму, надсилаючи запити на API endpoint.

## API Endpoints

### [POST] /decode
Виконує декодування штрих-коду з файлу зображення. Файл має бути надіслано у параметрі `file`.

### [GET] /decode
Виконує декодування штрих-коду з файлу, розташованого за посиланням. Посилання має бути надіслано у параметрі `url`.

### Формат відповіді
За замовчуванням у випадку успішного декодування повертаєтся HTML веб-сторінка наступного вигляду:
![preview](/success_example.PNG)

У випадку помилки, або якщо декодувати не вдалося, повертаєтся відповідне повідомлення:
![preview](/fail_example.PNG)

### Формат відповіді (JSON)
Також, якщо до запиту додати заголовок `Accept: application/json`, відповідь буде повернуто у форматі JSON:
```json
{
    "barcodeType": "EAN13",
    "value": "9578545203541",
    "binaryValue": "OTU3ODU0NTIwMzU0MQ==",
    "x1": 55,
    "y1": 137,
    "x2": 972,
    "y2": 509,
    "width": 917,
    "height": 372
}
```

Значення полів:
|          Поле          |                    Значення                     |
| :--------------------: | :---------------------------------------------- |
|      `barcodeType`     | Тип штрих-коду(QRCode, EAN13, ITF, тощо...)     |
|         `value`        | Текстове значення штрих-коду                    |
|     `binaryValue`      | Двійкове значення штрих-коду у кодуванні base64 |
| `x1`, `y1`, `x2`, `y2` | Координати штрих-коду на зображенні             |
|    `width` `height`    | Розміри штрих-коду на зображенні                |

Або ж у випадку помилки відповідь буде мати наступний вигляд:

```json
{
    "error": "Йосип драний! Сталась халепа..."
}
```