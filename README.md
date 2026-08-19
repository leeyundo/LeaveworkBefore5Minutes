# 퇴근 5분 전

Unity WebGL 데스크탑 타임어택 게임

## 개발 환경
- Unity 6 (6000.x)
- URP 2D
- WebGL
- TextMeshPro

## 현재 진행 상태

### 완료
- 프로젝트 생성
- GameDesign 작성
- TODO 작성

### 진행 예정
1. 프로젝트 세팅
2. 데스크탑 UI
3. Window 시스템
4. 파일 드래그
5. 업무 시스템

## 개발 규칙
- 한 번에 하나의 기능만 구현
- 완료 후 반드시 테스트
- 컴파일 오류 0개 유지
- 기능 완료 후 Git Commit
- UI 연결이 필요한 컴포넌트는 SerializeField를 사용한다.
- Inspector 연결을 우선하고 Find()는 사용하지 않는다.

## Unity 작업 규칙

- Codex는 Scene(.unity), Prefab(.prefab) 파일을 직접 수정하지 않는다.
- Codex는 C# Script만 생성/수정한다.
- Hierarchy 생성, UI 배치, Prefab 연결은 Unity Editor에서 직접 한다.

### 금지 사항

- .unity 파일 수정 금지
- .prefab 파일 생성/수정 금지

Codex는 C# 스크립트만 작성한다.
Prefab과 UI는 Unity Editor에서 직접 생성한다.