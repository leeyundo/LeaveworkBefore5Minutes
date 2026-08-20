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

## 진행상황

Unity 6 URP 2D 프로젝트 "LeaveworkBefore5Minutes" 작업 중.

현재 완료:
STEP 01~04 완료
STEP 05 코드 및 Unity Editor 작업 완료

현재 TODO:
1. 데스크탑 UI - 완료
2. Window 시스템 - 완료
3. 데스크탑 아이콘
   - 아이콘 프리팹 제작 완료
   - 싱글 클릭 선택 완료
   - 더블 클릭으로 창 열기 완료
   - 아이콘과 Window 연결 완료
4. 파일 시스템
   - FileItem 프리팹 완료
   - 드래그 완료
   - 드롭 완료
   - 파일 이름 표시 완료
5. 폴더 & 휴지통
   - Documents 창 완료
   - USB 창 완료
   - 휴지통 창 완료
   - 드롭 판정 완료

현재 상태:
DocumentsWindow, USBWindow, RecycleBinWindow을 WindowLayer 아래에 만들었고,
각 Content에 FileItem과 FileDropTarget을 연결함.
Target Type도 각각 Documents / USB / RecycleBin으로 설정함.
실행해서 드래그/드롭 테스트까지 완료했고 정상 작동 확인함.

다음 단계는 STEP 06 업무(Task) 시스템.