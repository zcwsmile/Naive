# Mini Search Engine Project

---

## 第一部分 草图

> * 抓取cnbeta.com的100条新闻
> * 对每篇新闻进行分词，挑选出最常出现的词
> * 去掉停用词之后，剩下的词作为关键词集合keywords.dat
> * 做url去重
> * 做文本去重
> * 对关键词建立倒排索引
> * 对输入的查询词query进行分词，计算出包含查询词的所有doc的id
> * 对输入的查询词query和上一步得到的doc计算相似度
> * 输出结果

## 第二部分 设计

### 1. 抓取cnbeta.com的新闻链接

```
Regex("/articles/\\d{6}.htm");
Regex("http://www.cnbeta.com/topics/\\d+.htm");
```

### 2. 抓取100篇新闻，直接拼接在一起，作为关键词集合的来源

```
Id("news_title");
Selector("//div[contains(@class,'introduction')]");
Selector("//div[contains(@class,'content')]");
InnerText()

```

### 3. 去掉停用词之后，选出出现超过2次的词作为关键词集合

停用词表从网上很容易找到

分词：https://github.com/aszxqw/libcppjieba/

### 4. 测试Url去重功能

```
Selector("//a[@href]");
```
*抓到了4万条之后，网络连接被校园网管理中心断开了*
这个以前写过的，现在不想写了，以后补

### 5. 文本去重


### 6. 试图抓取更多的科技网站新闻


### 7. 建立xml格式的文本库

xml组织方式说明
```xml
<docs>
    <doc>
        <link></link>
        <title></title>
        <abstract></abstract>
        <content></content>
    </doc>
    <doc>
        <link></link>
        <title></title>
        <abstract></abstract>
        <content></content>
    </doc>
    ...
</docs>
```
实现：

```C#
using System.Xml;

```

### 8. 对keywords建立倒排索引

索引格式为：

| 序号： | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
| ---- | ---- | ---- | ---- | ---- | ---- | ---- | ---- |
| 内容： | keyword | \t | index1 | \t | index2 | \t | ... |


对于每个关键词，找出所有它出现过的doc结点的起始位置，存储这个偏移量

