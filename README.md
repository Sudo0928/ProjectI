![Typing SVG](https://readme-typing-svg.demolab.com?font=Fira+Code&size=30&pause=1000&width=435&lines=MINI+ISAAC)
---
# 🛠️ Description
- **프로젝트 소개** <br>
  해당 프로젝트는 게임 <아이작>을 간단하게 모작하는 것을 목표로 시작한 프로젝트입니다. <br>
  짧은 시간동안 진행한 과제이기에 몬스터, 던전, 아이템 시스템 등 필수 구현 요소만 포함하여 작업을 진행하였습니다.
<br>

- **개발 기간** : 2025.02.21 - 2025.02.28
- **사용 기술** <br>
-언어 : C#<br>
-엔진 : Unity Engine <br>
<br>

# 🎮 Core Features 
- 던전 시스템
- 아이템 설계
- 보스 몬스터 패턴 
- 특수 기능
- 몬스터 설계
- 플레이어 로직
- User Interface

<br><br>
# 📜 Dungeon System
## 방 이동 시스템 <br>
![던전이동](https://github.com/user-attachments/assets/d2e2d1fa-b5c3-4f4f-b862-17cd24cdc57d)

- 던전의 각 방의 문 끼리 연결하는 방식으로 이동 로직을 구현했습니다. <br>
  플레이어가 문에 충돌하면, 연결된 다음 방의 문으로 이동하게 했습니다. <br>
  이후, 플레이어가 있는 방만 보이게 하였습니다.
<br><br>

## 방 자동 생성 시스템 <br>
![image](https://github.com/user-attachments/assets/7fabd256-2781-4117-bc02-08afac15f631)
- 방을 Prefab으로 만들 때, 모든 방향에 문을 설치해두었습니다. <br>
  오른 쪽 문으로 들어가면 다음 방의 왼쪽 문으로 나오게 되며, 위쪽 문으로 들어가면 아래쪽 문으로 나오게 되는 특징을 이용하였습니다. <br>
  현재 방에서 랜덤하게 방향을 선택하고, 해당 방향에 있는 문을 랜덤하게 선택합니다. <br>
  이후, 다음 방을 선택하고 현재 방향과 반대되는 방향에 있는 문을 선택하여 두 문을 연결하는 것을 반복하여 구현했습니다.
<br><br>

## 던전 보상 <br>
![image](https://github.com/user-attachments/assets/8b7c4787-e96a-4c1e-8e22-85ec8406c6ed)
- 방을 클리어하면 보상 아이템이 랜덤하게 생성되도록 하였습니다. <br>
  이 때, 엑셀에 정의한 아이템 데이터를 통해 확률이 적용되도록 했습니다.  
<br><br>


# 🎲 Item System
## 아이템 데이터
![image](https://github.com/user-attachments/assets/833cd1d2-d550-4126-80e1-8a3bebb652cc)
- 액셀을 이용하여 아이템 데이터를 정리 했습니다. <br>
  아이템의 이름, 등급, 아이템이 보유한 옵션 번호, 옵션의 수치, 특수 옵션과 설명, 메시지, 이미지 이름을 저장했습니다. <br>
###
![image](https://github.com/user-attachments/assets/dceaae3a-5f88-42a5-99e8-6b390976e698)
- 아이템 옵션과 특수 옵션도 엑셀 데이터를 통해 따로 관리를 하여 <br>
  아이템의 옵션 Key를 통해 옵션 정보를 꺼내올 수 있게 하였습니다. <br>
###
![image](https://github.com/user-attachments/assets/d69a9f50-86f2-4f47-abf1-b74c87f6c176)
<br><br>

## 아이템 특수 효과
![image](https://github.com/user-attachments/assets/2012d25d-7359-42e4-bee1-a01a7a0b9651)
- 아이템의 특수효과는 Component의 이름을 저장하였습니다.
###
![image](https://github.com/user-attachments/assets/fc4d3361-030c-44a4-a2a7-565ef4014ca8)
- 특수 효과에 해당하는 Component는 게임 시작시 리플렉션을 통해 미리 캐싱해두고, 필요시 꺼내쓰는 방식으로 구현했습니다. <br>
###
![image](https://github.com/user-attachments/assets/d0145c0a-a4dd-4998-abce-cdc3df12fa0f)
<br><br>

## 아이템


