# 采购相关的API

## 获取所有的采购清单信息，未设置分页

`get`请求，地址`/purchaselist`

返回JSON数据格式样例

```json
[
    {
        "id": "8da78208-5868-fe42-518e-b31dea4c88a5",
        "date": "2017-09-17T00:00:00",
        "cost": 3000000.0,
        "staffId": 2000022,
        "comment": "此处为备注",
        "purchaseListItems": [
            {
                "itemid": "326AG",
                "purchaseListItemType": "MedicialEquipment",
                "name": "核磁共振成像仪",
                "price": 3000000.0,
                "itemCount": 1,
                "producer": "GE",
                "description": null,
                "purchaseListId": "8da78208-5868-fe42-518e-b31dea4c88a5"
            }
        ]
    },
    {
        "id": "7c40b44c-af14-421d-90f3-261a4030562c",
        "date": "2022-08-01T00:00:00",
        "cost": 2210.0,
        "staffId": 2000022,
        "comment": "价廉质优",
        "purchaseListItems": [
            {
                "itemid": "H19984016",
                "purchaseListItemType": "Medicine",
                "name": "盘尼西林",
                "price": 9.6,
                "itemCount": 100,
                "producer": "同仁堂药业",
                "description": "信誉良好制药公司",
                "purchaseListId": "7c40b44c-af14-421d-90f3-261a4030562c"
            },
            {
                "itemid": "H20060016",
                "purchaseListItemType": "Medicine",
                "name": "三七粉",
                "price": 12.5,
                "itemCount": 100,
                "producer": "云南制药",
                "description": "手工磨制，成本较高",
                "purchaseListId": "7c40b44c-af14-421d-90f3-261a4030562c"
            }
        ]
    },
    {
        "id": "bdb5a3ab-2173-2650-90b3-00ce06475921",
        "date": "2022-02-03T00:00:00",
        "cost": 913.0,
        "staffId": 2000021,
        "comment": null,
        "purchaseListItems": [
            {
                "itemid": "H19994016",
                "purchaseListItemType": "Medicine",
                "name": "阿莫西林克拉维酸钾片",
                "price": 9.6,
                "itemCount": 30,
                "producer": "长江制药有限公司",
                "description": null,
                "purchaseListId": "bdb5a3ab-2173-2650-90b3-00ce06475921"
            },
            {
                "itemid": "H20040016",
                "purchaseListItemType": "Medicine",
                "name": "地红霉素肠溶胶囊",
                "price": 12.5,
                "itemCount": 50,
                "producer": "长春制药",
                "description": null,
                "purchaseListId": "bdb5a3ab-2173-2650-90b3-00ce06475921"
            }
        ]
    }
]
```

## 由编号获取某一采购信息

`get`请求，地址`/purchaselist/{uuid}`

返回JSON数据格式样例

```json
// 请求  https://localhost:5001/purchaselist/8da78208-5868-fe42-518e-b31dea4c88a5

{
    "id": "8da78208-5868-fe42-518e-b31dea4c88a5",
    "date": "2017-09-17T00:00:00",
    "cost": 3000000.0,
    "staffId": 2000022,
    "comment": "此处为备注",
    "purchaseListItems": [
        {
            "itemid": "326AG",
            "purchaseListItemType": "MedicialEquipment",
            "name": "核磁共振成像仪",
            "price": 3000000.0,
            "itemCount": 1,
            "producer": "GE",
            "description": null,
            "purchaseListId": "8da78208-5868-fe42-518e-b31dea4c88a5"
        }
    ]
}
```

## 或取某一编号的采购项

`get`请求，地址`/purchaselist/{uuid}/items`

返回JSON数据格式样例

```json
// 请求 https://localhost:5001/purchaselist/7c40b44c-af14-421d-90f3-261a4030562c/items

[
    {
        "itemid": "H19984016",
        "purchaseListItemType": "Medicine",
        "name": "盘尼西林",
        "price": 9.6,
        "itemCount": 100,
        "producer": "同仁堂药业",
        "description": "信誉良好制药公司",
        "purchaseListId": "7c40b44c-af14-421d-90f3-261a4030562c"
    },
    {
        "itemid": "H20060016",
        "purchaseListItemType": "Medicine",
        "name": "三七粉",
        "price": 12.5,
        "itemCount": 100,
        "producer": "云南制药",
        "description": "手工磨制，成本较高",
        "purchaseListId": "7c40b44c-af14-421d-90f3-261a4030562c"
    }
]
```


## 由员工编号获取相应采购单信息

`get`请求，地址`/purchaselist?staffid={staffid}`

返回JSON数据格式样例

```json
// 请求 https://localhost:5001/purchaselist?staffid=2000022
[
    {
        "id": "8da78208-5868-fe42-518e-b31dea4c88a5",
        "date": "2017-09-17T00:00:00",
        "cost": 3000000.0,
        "staffId": 2000022,
        "comment": "此处为备注",
        "purchaseListItems": [
            {
                "itemid": "326AG",
                "purchaseListItemType": "MedicialEquipment",
                "name": "核磁共振成像仪",
                "price": 3000000.0,
                "itemCount": 1,
                "producer": "GE",
                "description": null,
                "purchaseListId": "8da78208-5868-fe42-518e-b31dea4c88a5"
            }
        ]
    },
    {
        "id": "7c40b44c-af14-421d-90f3-261a4030562c",
        "date": "2022-08-01T00:00:00",
        "cost": 2210.0,
        "staffId": 2000022,
        "comment": "价廉质优",
        "purchaseListItems": [
            {
                "itemid": "H19984016",
                "purchaseListItemType": "Medicine",
                "name": "盘尼西林",
                "price": 9.6,
                "itemCount": 100,
                "producer": "同仁堂药业",
                "description": "信誉良好制药公司",
                "purchaseListId": "7c40b44c-af14-421d-90f3-261a4030562c"
            },
            {
                "itemid": "H20060016",
                "purchaseListItemType": "Medicine",
                "name": "三七粉",
                "price": 12.5,
                "itemCount": 100,
                "producer": "云南制药",
                "description": "手工磨制，成本较高",
                "purchaseListId": "7c40b44c-af14-421d-90f3-261a4030562c"
            }
        ]
    }
]
```

## 上传采购清单

`post`请求， 地址`/purchaselist`

body部分所需数据格式

```json
{
    "date": "2022-02-03",
    "cost": 913.0,
    "staffId": 2000021,
    "comment": null,
    "purchaseListItems": [
        {
            "itemid": "H19994016",
            "purchaseListItemType": "Medicine",
            "name": "阿莫西林克拉维酸钾片",
            "price": 9.6,
            "itemCount": 30,
            "producer": "长江制药有限公司",
            "description": null
        },
        {
            "itemid": "H20040016",
            "purchaseListItemType": "Medicine",
            "name": "地红霉素肠溶胶囊",
            "price": 12.5,
            "itemCount": 50,
            "producer": "长春制药",
            "description": null
        }
    ]
}
```
