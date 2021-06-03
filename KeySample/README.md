# KeySample project for Xamarin.Forms

## 基本構造

### ソフトウエアキーボード

* ソフトウエアキーボードはOSレベルで無効化を想定
* EntryRendererのキーボード処理を潰すのが大がかりなので(やろうと思えばできる)

### ショートカット

* ボタンに対するショートカットキー(0～9、Del、FnX)割り当て

### タブ移動

* 標準のタブ機構は使い物にならないので自前実装し、上/下キーで前/次コントロールへ移動
* 標準機能はButtonのFastRendererを考慮していなかったり、IsEnabled=falseなコンテナ上のボタン等にも移動してしまったり、移動順序がおかしい？
* 自前機構としては、とりあえず項目のレイアウト順のみを考慮だがTabオーダー対応やReferenceでの明示的指定も可能

### Android側キー制御

* 優先してキーフックするためDispatchKeyEvent()でキーフック
* Entry、ListView等、独自のキー制御が入るものについては、キー制御が入る条件の時はフックを除外 

### コントロール全般

* 標準でフォーカス移動の対応に問題があるコントロール(Button、ListView等)はRendererレベルで全て設定を変更

### Entry

* Completeボタンの標準挙動ではソフトキーボード制御に難があるので自前の制御で上書き
* Text、Enable、フォーカス制御をまとめたEntryModelを用意し、EntryBind.ModelでバインドすることでAll in oneで設定

### Button、CheckBox

* 標準ではFocus制御に対応していないので設定を上書き

### ListView

* 標準ではFocus制御に対応していないので設定を上書き
* 選択色のカスタマイズ

### Dialog(メッセージ表示、一覧選択)

* [DEL]キーによるキャンセルに対応
* 初期状態でのボタンへのフォーカス設定
* 一覧選択での初期値設定、選択色のカスタマイズ

### Popup

* Rg.Plugins.PopupのPopupPageへの対応
* PopupPageに対しては固有のハンドラをスタックする形で対応

### バーコード(Entry単位)

* IEntryBarcodeReader及びBarcodeEntryBehavior参照
* BarcodeEntryBehaviorによりフォーカス時にIEntryBarcodeReaderに対してEntryを登録し、スキャンイベントが発生したら通知してもらう形
