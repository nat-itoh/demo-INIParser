using System;
using System.IO;
using System.Text;
using IniParser;
using IniParser.Model;

// [REF]
//  github: rickyah/ini-parser https://github.com/rickyah/ini-parser

namespace IniParser.Model {
    
    /// <summary>
    /// <see cref="KeyDataCollection"/>型の拡張メソッド集．
    /// </summary>
    public static class KeyDataCollectionExtensions {

        /// <summary>
        /// 指定したキーから値を取得し、存在しない場合はデフォルト値を返す．
        /// </summary>
        public static string GetOrDefault(this KeyDataCollection self, string key, string defaultValue = "") {
            return self.ContainsKey(key) ? self[key] : defaultValue;
        }

        /// <summary>
        /// 指定したキーから値を取得し、存在しない場合や型変換に失敗した場合はデフォルト値を返す．
        /// </summary>
        /// <typeparam name="T">取得する値の型</typeparam>
        /// <param name="self">KeyDataCollection インスタンス</param>
        /// <param name="key">キー名</param>
        /// <param name="defaultValue">デフォルト値 (省略可能)</param>
        /// <returns>取得した値またはデフォルト値</returns>
        public static T GetOrDefault<T>(this KeyDataCollection self, string key, T defaultValue = default) {
            Func<string, T> parser = value => (T)Convert.ChangeType(value, typeof(T));
            return self.GetOrDefault<T>(key, parser, defaultValue);
        }

        /// <summary>
        /// 指定したキーから値を取得し、指定されたパーサーで変換する．
        /// 変換に失敗した場合やキーが存在しない場合はデフォルト値を返す．
        /// </summary>
        /// <typeparam name="T">取得する値の型</typeparam>
        /// <param name="self">KeyDataCollection インスタンス</param>
        /// <param name="key">キー名</param>
        /// <param name="parser">文字列を T 型に変換する関数</param>
        /// <param name="defaultValue">デフォルト値 (省略可能)</param>
        /// <returns>取得した値またはデフォルト値</returns>
        public static T GetOrDefault<T>(this KeyDataCollection self, string key, Func<string, T> parser, T defaultValue = default) {
            if (!self.ContainsKey(key))
                return defaultValue;

            try {
                return parser(self[key]);
            } catch {
                return defaultValue;
            }
        }
    }


    public static class IniDataExtensions {

        /// <summary>
        /// 指定したセクションとキーから値を取得し、存在しない場合はデフォルト値を返します。
        /// </summary>
        /// <param name="iniData">解析済みのIniData</param>
        /// <param name="section">セクション名</param>
        /// <param name="key">キー名</param>
        /// <param name="defaultValue">デフォルト値 (省略可能)</param>
        /// <returns>取得した値またはデフォルト値</returns>
        public static string GetOrDefault(this IniData iniData, string section, string key, string defaultValue = "") {
            if (iniData.Sections.ContainsSection(section) && iniData[section].ContainsKey(key)) {
                return iniData[section][key];
            }
            return defaultValue;
        }

        /// <summary>
        /// 指定したセクションとキーから値を取得し、存在しない場合や型変換に失敗した場合はデフォルト値を返します。
        /// </summary>
        /// <typeparam name="T">取得する値の型</typeparam>
        /// <param name="iniData">解析済みのIniData</param>
        /// <param name="section">セクション名</param>
        /// <param name="key">キー名</param>
        /// <param name="defaultValue">デフォルト値 (省略可能)</param>
        /// <returns>取得した値またはデフォルト値</returns>
        public static T GetOrDefault<T>(this IniData iniData, string section, string key, T defaultValue = default) {
            if (iniData.Sections.ContainsSection(section) && iniData[section].ContainsKey(key)) {
                var value = iniData[section][key];

                try {
                    // 型変換を試みる
                    return (T)Convert.ChangeType(value, typeof(T));
                } catch {
                    // 型変換に失敗した場合はデフォルト値を返す
                    return defaultValue;
                }
            }
            return defaultValue;
        }
    }


}