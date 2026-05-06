# GameEnginePrograming

게임엔진프로그래밍 수업/실습을 위한 Unity 프로젝트입니다. 공포/던전 분위기의 3D 씬, 몬스터 로직, 플레이어 공격, 손전등, 엔딩 씬 등 게임 엔진 기능을 실험합니다.

## 프로젝트 개요

`GameEnginePrograming`은 Unity 엔진에서 3D 게임 씬과 상호작용을 구현해보는 실습 저장소입니다. 여러 외부 에셋과 직접 작성한 `Assets/JJH/Scripts` 스크립트를 조합해 게임 흐름을 구성합니다.

## 주요 구현 영역

- 몬스터 AI와 몬스터별 동작 스크립트
- 플레이어 공격 처리
- 손전등 기능
- 게임 매니저와 씬 전환 관리
- 상호작용 매니저
- 시작/탈출/히든/사망 엔딩 씬 구성
- 공포 게임용 배경과 오브젝트 에셋 활용

## 기술 스택

- Unity
- C#
- Unity Scene 시스템
- Unity Standard Assets
- 3D 환경/몬스터 에셋

## 주요 폴더

```text
.
├── Assets/
│   ├── JJH/Scripts/
│   │   ├── GameManager.cs
│   │   ├── MonsterAI.cs
│   │   ├── PlayerAttack.cs
│   │   ├── PlayerFlashlight.cs
│   │   └── *Logic.cs
│   ├── Scenes/
│   └── Standard Assets/
├── Packages/
├── ProjectSettings/
└── README.md
```

## 실행 방법

1. Unity Hub에서 프로젝트를 엽니다.
2. `Assets/Scenes/StartScene.unity` 또는 원하는 씬을 엽니다.
3. 패키지와 에셋 임포트가 끝난 뒤 Play 버튼으로 실행합니다.

## 씬 예시

- `StartScene.unity`
- `EscapeScene.unity`
- `DeadEndingScene.unity`
- `HiddenEndingScene.unity`
- `JJH3.unity`

## 개발 메모

이 저장소는 게임엔진 기능 실험과 수업 과제 성격이 강합니다. README를 더 발전시키려면 Unity 버전, 조작법, 게임 목표, 엔딩 조건, 직접 구현한 스크립트 설명을 추가하는 것이 좋습니다.
