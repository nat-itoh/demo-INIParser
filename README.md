# demo-INIParser

## 概要
`INI File Parser`を使用してUnity上でINIファイルを扱うデモ．


## INI File Parser

[リポジトリ](https://github.com/rickyah/ini-parser)

- 
- `Windows API`に依存していない

#### ファイル読み込み

ライブラリではINIファイルを`IniParser.Model.IniData`クラスとして扱う.`FileIniDataParser`のインスタンスを生成して、Readメソッドでデータを読み込む．

```cs
var parser = new FileIniDataParser();
IniData data = parser.ReadFile("Configuration.ini");
```

名前付きセクション内のキーの値を取得します。値は常に `string`として取得される．$$

```cs
```

#### ファイル書き込み

```cs
data["UI"]["fullscreen"] = "true";
parser.WriteFile("Configuration.ini", data);
```

